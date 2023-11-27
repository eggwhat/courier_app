using Convey.CQRS.Events;

namespace SwiftParcel.Services.Orders.Application.Events.External
{
    public class DeliveryFailed : IEvent
    {
        public Guid DeliveryId { get; }
        public Guid OrderId { get; }
        public string Reason { get; }

        public DeliveryFailed(Guid deliveryId, Guid orderId, string reason)
        {
            DeliveryId = deliveryId;
            OrderId = orderId;
            Reason = reason;
        }
    }
}