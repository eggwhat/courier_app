using System.Threading.Tasks;
using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Application.Services;
using SwiftParcel.Services.Orders.Application.Events;
using SwiftParcel.Services.Orders.Application.Exceptions;
using SwiftParcel.Services.Orders.Application.Services.Clients;
using SwiftParcel.Services.Orders.Core.Repositories;

namespace SwiftParcel.Services.Orders.Application.Commands.Handlers
{
    public class AssignCourierToOrderHandler : ICommandHandler<AssignCourierToOrder>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPricingServiceClient _pricingServiceClient;
        private readonly ICouriersServiceClient _couriersServiceClient;
        private readonly IMessageBroker _messageBroker;
        private readonly IAppContext _appContext;

        public AssignCourierToOrderHandler(IOrderRepository orderRepository,
            IPricingServiceClient pricingServiceClient, ICouriersServiceClient couriersServiceClient,
            IMessageBroker messageBroker, IAppContext appContext)
        {
            _orderRepository = orderRepository;
            _pricingServiceClient = pricingServiceClient;
            _couriersServiceClient = couriersServiceClient;
            _messageBroker = messageBroker;
            _appContext = appContext;
        }
        public async Task HandleAsync(AssignCourierToOrder command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(command.OrderId);
            if (order is null)
            {
                throw new OrderNotFoundException(command.OrderId);
            }

            var identity = _appContext.Identity;
            if (identity.IsAuthenticated && identity.Id != order.CustomerId && !identity.IsAdmin)
            {
                throw new UnauthorizedOrderAccessException(command.OrderId, identity.Id);
            }

            if (!order.HasParcels)
            {
                throw new OrderHasNoParcelsException(command.OrderId);
            }

            if (!order.CanAssignCourier)
            {
                return;
            }

            var courier = await _couriersServiceClient.GetAsync(command.CourierId);
            if (courier is null)
            {
                throw new CourierNotFoundException(command.CourierId);
            }
            
            var pricing = await _pricingServiceClient.GetOrderPriceAsync(order.CustomerId, courier.PricePerService);
            order.SetCourier(command.CourierId);
            order.SetTotalPrice(pricing.OrderDiscountPrice);
            order.SetDeliveryDate(command.DeliveryDate);
            await _orderRepository.UpdateAsync(order);
            await _messageBroker.PublishAsync(new CourierAssignedToOrder(command.OrderId, command.CourierId));
        }
    }
}