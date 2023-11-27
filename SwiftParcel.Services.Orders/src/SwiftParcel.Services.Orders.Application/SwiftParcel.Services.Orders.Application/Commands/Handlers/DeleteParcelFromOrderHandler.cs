using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Application.Services;
using SwiftParcel.Services.Orders.Core.Entities;
using SwiftParcel.Services.Orders.Application.Exceptions;

namespace SwiftParcel.Services.Orders.Application.Commands.Handlers
{
    public class DeleteParcelFromOrderHandler: ICommandHandler<DeleteParcelFromOrder>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAppContext _appContext;
        private readonly IMessageBroker _messageBroker;
        private readonly IEventMapper _eventMapper;

        public DeleteParcelFromOrderHandler(IOrderRepository orderRepository, IAppContext appContext,
            IMessageBroker messageBroker, IEventMapper eventMapper)
        {
            _orderRepository = orderRepository;
            _appContext = appContext;
            _messageBroker = messageBroker;
            _eventMapper = eventMapper;
        }
        public async Task HandleAsync(DeleteParcelFromOrder command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(command.OrderId);
            if (order is null)
            {
                throw new OrderNotFoundException(command.OrderId);
            }

            var identity = _appContext.Identity;
            if (identity.IsAuthenticated && identity.Id != order.CustomerId && !identity.IsAdmin)
            {
                throw new UnauthorizedOrderAccessException(order.Id, identity.Id);
            }

            order.DeleteParcel(command.ParcelId);
            await _orderRepository.UpdateAsync(order);
            var events = _eventMapper.MapAll(order.Events);
            await _messageBroker.PublishAsync(events.ToArray());
        }
    }
}