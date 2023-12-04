using Convey.CQRS.Events;

namespace SwiftParcel.Services.Deliveries.Application.Events
{
    public class DeliveryStarted : IEvent
    {
        public Guid DeliveryId { get; }
        public Guid OrderId { get; }
        public DateTime DateTime { get; }

        public DeliveryStarted(Guid id, Guid orderId, DateTime dateTime)
        {
            DeliveryId = id;
            OrderId = orderId;
            DateTime = dateTime;
        }
    }
}