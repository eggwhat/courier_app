using Convey.CQRS.Events;
using SwiftParcel.Services.Deliveries.Core.Entities;
using SwiftParcel.Services.Deliveries.Core.Events;
using SwiftParcel.Services.Deliveries.Application.Events;
using SwiftParcel.Services.Deliveries.Application.Services;

namespace SwiftParcel.Services.Deliveries.Infrastructure.Services
{
    public class EventMapper : IEventMapper
    {
        public IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events)
            => events.Select(Map);

        public IEvent Map(IDomainEvent @event)
        {
            switch (@event)
            {
                case DeliveryStateChanged e:
                    switch (e.Delivery.Status)
                    {
                        case DeliveryStatus.InProgress:
                            return new DeliveryStarted(e.Delivery.Id, e.Delivery.OrderId, e.dateTime);
                        case DeliveryStatus.Completed:
                            return new DeliveryCompleted(e.Delivery.Id, e.Delivery.OrderId, e.dateTime);
                        case DeliveryStatus.CannotDeliver:
                            return new DeliveryFailed(e.Delivery.Id, e.Delivery.OrderId, e.dateTime, e.Delivery.Notes);
                    }
                    break;
                case DeliveryRegistrationAdded e:
                    return new RegistrationAddedToDelivery(e.Delivery.Id, e.Delivery.OrderId, e.Registration.Description);
            }

            return null;
        }
    }
}