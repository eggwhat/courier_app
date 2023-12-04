using Convey.CQRS.Commands;
using SwiftParcel.Services.Deliveries.Application.Exceptions;
using SwiftParcel.Services.Deliveries.Application.Services;
using SwiftParcel.Services.Deliveries.Core.Repositories;
using SwiftParcel.Services.Deliveries.Core.ValueObjects;


namespace SwiftParcel.Services.Deliveries.Application.Commands.Handlers
{
    internal sealed class AddDeliveryRegistrationHandler : ICommandHandler<AddDeliveryRegistration>
    {
        private readonly IDeliveriesRepository _repository;
        private readonly IMessageBroker _messageBroker;
        private readonly IEventMapper _eventMapper;

        public AddDeliveryRegistrationHandler(IDeliveriesRepository repository, IMessageBroker messageBroker,
            IEventMapper eventMapper)
        {
            _repository = repository;
            _messageBroker = messageBroker;
            _eventMapper = eventMapper;
        }

        public async Task HandleAsync(AddDeliveryRegistration command)
        {
            var delivery = await  _repository.GetAsync(command.DeliveryId);
            if (delivery is null)
            {
                throw new DeliveryNotFoundException(command.DeliveryId);
            }
            
            delivery.AddRegistration(new DeliveryRegistration(command.Description, command.DateTime));
            if (delivery.HasChanged)
            {
                await _repository.UpdateAsync(delivery);
                var events = _eventMapper.MapAll(delivery.Events);
                await _messageBroker.PublishAsync(events.ToArray());
            }
        }
    }
}