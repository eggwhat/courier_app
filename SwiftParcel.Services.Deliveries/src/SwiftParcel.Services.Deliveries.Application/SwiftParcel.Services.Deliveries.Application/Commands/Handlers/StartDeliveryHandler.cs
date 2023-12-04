using Convey.CQRS.Commands;
using SwiftParcel.Services.Deliveries.Application.Exceptions;
using SwiftParcel.Services.Deliveries.Application.Services;
using SwiftParcel.Services.Deliveries.Core.Entities;
using SwiftParcel.Services.Deliveries.Core.Repositories;
using SwiftParcel.Services.Deliveries.Core.ValueObjects;

namespace SwiftParcel.Services.Deliveries.Application.Commands.Handlers
{
    internal sealed class StartDeliveryHandler : ICommandHandler<StartDelivery>
    {
        private readonly IDeliveriesRepository _repository;
        private readonly IMessageBroker _messageBroker;
        private readonly IEventMapper _eventMapper;

        public StartDeliveryHandler(IDeliveriesRepository repository, IMessageBroker messageBroker,
            IEventMapper eventMapper)
        {
            _repository = repository;
            _messageBroker = messageBroker;
            _eventMapper = eventMapper;
        }

        public async Task HandleAsync(StartDelivery command)
        {
            var delivery = await _repository.GetForOrderAsync(command.OrderId);
            if (delivery is {} && delivery.Status != DeliveryStatus.CannotDeliver)
            {
                throw new DeliveryAlreadyStartedException(command.OrderId);
            }

            delivery = Delivery.Create(command.DeliveryId, command.OrderId, DeliveryStatus.InProgress);
            delivery.AddRegistration(new DeliveryRegistration(command.Description, command.DateTime));
            await _repository.AddAsync(delivery);
            var events = _eventMapper.MapAll(delivery.Events);
            await _messageBroker.PublishAsync(events.ToArray());
        }
    }
}