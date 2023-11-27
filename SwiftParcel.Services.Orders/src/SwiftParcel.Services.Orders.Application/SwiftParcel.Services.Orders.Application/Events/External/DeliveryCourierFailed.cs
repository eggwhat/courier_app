using Convey.CQRS.Events;

namespace SwiftParcel.Services.Orders.Application.Events.External
{
    public class DeliveryCourierFailed : IEvent
    {
        public Guid DeliveryId { get; }
        public Guid OrderId { get; }
        public DateTime DateTime { get; }
        public string Reason { get; }

        public DeliveryCourierFailed(Guid deliveryId, Guid orderId, DateTime dateTime, string reason)
        {
            DeliveryId = deliveryId;
            OrderId = orderId;
            DateTime = dateTime;
            Reason = reason;
        }
    }
}