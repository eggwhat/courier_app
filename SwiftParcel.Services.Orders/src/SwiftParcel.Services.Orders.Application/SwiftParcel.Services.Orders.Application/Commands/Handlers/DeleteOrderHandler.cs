using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Application.Services;
using SwiftParcel.Services.Orders.Core.Entities;
using SwiftParcel.Services.Orders.Application.Exceptions;
using SwiftParcel.Services.Orders.Core.Exceptions;
using SwiftParcel.Services.Orders.Application.Events;
using SwiftParcel.Services.Orders.Core.Repositories;


namespace SwiftParcel.Services.Orders.Application.Commands.Handlers
{
    public class DeleteOrderHandler: ICommandHandler<DeleteOrder>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAppContext _appContext;
        private readonly IMessageBroker _messageBroker;
        public DeleteOrderHandler(IOrderRepository orderRepository, IAppContext appContext,
            IMessageBroker messageBroker)
        {
            _orderRepository = orderRepository;
            _appContext = appContext;
            _messageBroker = messageBroker;
        }
        public async Task HandleAsync(DeleteOrder command, CancellationToken cancellationToken)
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

            if (!order.CanBeDeleted)
            {
                throw new CannotDeleteOrderException(command.OrderId);
            }

            await _orderRepository.DeleteAsync(command.OrderId);
            await _messageBroker.PublishAsync(new OrderDeleted(command.OrderId));
        }
    }
    
}