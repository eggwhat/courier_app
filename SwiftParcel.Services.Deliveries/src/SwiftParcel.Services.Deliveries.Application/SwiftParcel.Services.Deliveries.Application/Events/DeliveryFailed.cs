using Convey.CQRS.Events;

namespace SwiftParcel.Services.Deliveries.Application.Events
{
    public class DeliveryFailed : IEvent
    {
        public Guid DeliveryId { get; }
        public Guid OrderId { get; }
        public DateTime DateTime { get; }
        public string Reason { get; }

        public DeliveryFailed(Guid deliveryId, Guid orderId, DateTime dateTime, string reason)
        {
            DeliveryId = deliveryId;
            OrderId = orderId;
            DateTime = dateTime;
            Reason = reason;
        }
    }
}