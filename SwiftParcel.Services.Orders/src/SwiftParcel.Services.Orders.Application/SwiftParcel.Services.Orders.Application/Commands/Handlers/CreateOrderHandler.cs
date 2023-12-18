using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Application.Services;
using SwiftParcel.Services.Orders.Core.Entities;
using SwiftParcel.Services.Orders.Core.Repositories;
using SwiftParcel.Services.Orders.Core.Exceptions;
using SwiftParcel.Services.Orders.Application.Services.Clients;
using SwiftParcel.Services.Orders.Application.Exceptions;


namespace SwiftParcel.Services.Orders.Application.Commands.Handlers
{
    public class CreateOrderHandler : ICommandHandler<CreateOrder>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly IEventMapper _eventMapper;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IParcelsServiceClient _parcelsServiceClient;

        public CreateOrderHandler(IOrderRepository orderRepository, ICustomerRepository customerRepository,
            IMessageBroker messageBroker, IEventMapper eventMapper, IDateTimeProvider dateTimeProvider, 
             IParcelsServiceClient parcelsServiceClient)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _messageBroker = messageBroker;
            _eventMapper = eventMapper;
            _dateTimeProvider = dateTimeProvider;
            _parcelsServiceClient = parcelsServiceClient;
        }
        public async Task HandleAsync(CreateOrder command, CancellationToken cancellationToken)
        {
            if (command.CustomerId != Guid.Empty && !await _customerRepository.ExistsAsync(command.CustomerId))
            {
                throw new CustomerNotFoundException(command.CustomerId);
            }

            var parcelDto = await _parcelsServiceClient.GetAsync(command.ParcelId);
            if (parcelDto is null)
            {
                throw new ParcelNotFoundException(command.ParcelId);
            }
            var parcel = new Parcel(command.ParcelId, parcelDto.Description, 
                            parcelDto.Width, parcelDto.Height, parcelDto.Depth, parcelDto.Weight, parcelDto.Source, parcelDto.Destination,
                            parcelDto.Priority, parcelDto.AtWeekend, parcelDto.PickupDate, parcelDto.DeliveryDate, parcelDto.IsCompany, 
                            parcelDto.VipPackage, parcelDto.CreatedAt, parcelDto.ValidTo, parcelDto.CalculatedPrice);
            var requestDate = _dateTimeProvider.Now;
            parcel.ValidateRequest(requestDate);

            var order = Order.Create(command.OrderId, command.CustomerId, OrderStatus.New, requestDate,
                command.Name, command.Email, command.Address);
            order.AddParcel(parcel);

            await _orderRepository.AddAsync(order);
            var events = _eventMapper.MapAll(order.Events);
            await _messageBroker.PublishAsync(events.ToArray());
        }
    }
}