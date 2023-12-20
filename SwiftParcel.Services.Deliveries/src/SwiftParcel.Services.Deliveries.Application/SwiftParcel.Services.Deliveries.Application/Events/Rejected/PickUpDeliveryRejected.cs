using Convey.CQRS.Events;

namespace SwiftParcel.Services.Deliveries.Application.Events.Rejected
{
    public class PickUpDeliveryRejected : IRejectedEvent
    {
        public Guid DeliveryId { get; }
        public string Reason { get; }
        public string Code { get; }

        public PickUpDeliveryRejected(Guid deliveryId, string reason, string code)
        {
            DeliveryId = deliveryId;
            Reason = reason;
            Code = code;
        }
    }
}