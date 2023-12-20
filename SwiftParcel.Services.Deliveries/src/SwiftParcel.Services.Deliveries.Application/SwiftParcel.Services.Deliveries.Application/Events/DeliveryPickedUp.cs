using Convey.CQRS.Events;

namespace SwiftParcel.Services.Deliveries.Application.Events
{
    public class DeliveryPickedUp : IEvent
    {
        public Guid DeliveryId { get; }
        public Guid OrderId { get; }
        public DateTime DateTime { get; }

        public DeliveryPickedUp(Guid deliveryId, Guid orderId, DateTime dateTime)
        {
            DeliveryId = deliveryId;
            OrderId = orderId;
            DateTime = dateTime;
        }
    }
}