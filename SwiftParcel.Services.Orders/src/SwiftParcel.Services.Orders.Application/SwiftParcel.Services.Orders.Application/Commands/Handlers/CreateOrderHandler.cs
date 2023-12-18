using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Application.Services;
using SwiftParcel.Services.Orders.Core.Entities;
using SwiftParcel.Services.Orders.Core.Repositories;
using SwiftParcel.Services.Orders.Core.Exceptions;


namespace SwiftParcel.Services.Orders.Application.Commands.Handlers
{
    public class CreateOrderHandler : ICommandHandler<CreateOrder>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly IEventMapper _eventMapper;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ICommandDispatcher _commandDispatcher;

        public CreateOrderHandler(IOrderRepository orderRepository, ICustomerRepository customerRepository,
            IMessageBroker messageBroker, IEventMapper eventMapper, IDateTimeProvider dateTimeProvider, 
            ICommandDispatcher commandDispatcher)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _messageBroker = messageBroker;
            _eventMapper = eventMapper;
            _dateTimeProvider = dateTimeProvider;
            _commandDispatcher = commandDispatcher;
        }
        public async Task HandleAsync(CreateOrder command, CancellationToken cancellationToken)
        {
            if (command.CustomerId != Guid.Empty && !await _customerRepository.ExistsAsync(command.CustomerId))
            {
                throw new CustomerNotFoundException(command.CustomerId);
            }

            var order = Order.Create(command.OrderId, command.CustomerId, OrderStatus.New, _dateTimeProvider.Now,
                command.Name, command.Email, command.Address);
            await _orderRepository.AddAsync(order);
            var events = _eventMapper.MapAll(order.Events);
            await _messageBroker.PublishAsync(events.ToArray());
        }
    }
}