using Convey.CQRS.Events;

namespace SwiftParcel.Services.Deliveries.Application.Events.Rejected
{
    public class CompleteDeliveryRejected : IRejectedEvent
    {
        public Guid DeliveryId { get; }
        public string Reason { get; }
        public string Code { get; }

        public CompleteDeliveryRejected(Guid deliveryId, string reason, string code)
        {
            DeliveryId = deliveryId;
            Reason = reason;
            Code = code;
        }
    }
}