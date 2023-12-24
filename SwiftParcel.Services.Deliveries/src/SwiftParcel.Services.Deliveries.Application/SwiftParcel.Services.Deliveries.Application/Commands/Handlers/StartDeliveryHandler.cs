using Convey.CQRS.Commands;
using SwiftParcel.Services.Deliveries.Application.Exceptions;
using SwiftParcel.Services.Deliveries.Application.Services;
using SwiftParcel.Services.Deliveries.Core.Entities;
using SwiftParcel.Services.Deliveries.Core.Repositories;

namespace SwiftParcel.Services.Deliveries.Application.Commands.Handlers
{
    internal sealed class StartDeliveryHandler : ICommandHandler<StartDelivery>
    {
        private readonly IDeliveriesRepository _repository;
        private readonly IMessageBroker _messageBroker;
        private readonly IEventMapper _eventMapper;
        private readonly IDateTimeProvider _dateTimeProvider;

        public StartDeliveryHandler(IDeliveriesRepository repository, IMessageBroker messageBroker,
            IEventMapper eventMapper, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _messageBroker = messageBroker;
            _eventMapper = eventMapper;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task HandleAsync(StartDelivery command)
        {
            var delivery = await _repository.GetForOrderAsync(command.OrderId);
            if (delivery is {} && delivery.Status != DeliveryStatus.CannotDeliver)
            {
                throw new DeliveryAlreadyStartedException(command.OrderId);
            }

            delivery = Delivery.Create(command.DeliveryId, command.OrderId, _dateTimeProvider.Now,
                DeliveryStatus.Unassigned, command.Volume, command.Weight, command.Source, command.Destination,
                command.Priority, command.AtWeekend, command.PickupDate, command.DeliveryDate);
            await _repository.AddAsync(delivery);
            var events = _eventMapper.MapAll(delivery.Events);
            await _messageBroker.PublishAsync(events.ToArray());
        }
    }
}