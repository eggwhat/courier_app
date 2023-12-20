using Convey.CQRS.Commands;
using SwiftParcel.Services.Deliveries.Application.Exceptions;
using SwiftParcel.Services.Deliveries.Application.Services;
using SwiftParcel.Services.Deliveries.Core.Repositories;

namespace SwiftParcel.Services.Deliveries.Application.Commands.Handlers
{
    internal sealed class AssignCourierToDeliveryHandler: ICommandHandler<AssignCourierToDelivery>
    {
        private readonly IDeliveriesRepository _repository;
        private readonly IMessageBroker _messageBroker;
        private readonly IEventMapper _eventMapper;
        private readonly IDateTimeProvider _dateTimeProvider;
        public AssignCourierToDeliveryHandler(IDeliveriesRepository repository, IMessageBroker messageBroker, 
            IEventMapper eventMapper, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _messageBroker = messageBroker;
            _eventMapper = eventMapper;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task HandleAsync(AssignCourierToDelivery command)
        {
            var delivery = await _repository.GetAsync(command.DeliveryId);
            if (delivery is null)
            {
                throw new DeliveryNotFoundException(command.DeliveryId);
            }

            delivery.AssignCourier(_dateTimeProvider.Now, command.CourierId);
            await _repository.UpdateAsync(delivery);
            var events = _eventMapper.MapAll(delivery.Events);
            await _messageBroker.PublishAsync(events.ToArray());
        }
    }
}