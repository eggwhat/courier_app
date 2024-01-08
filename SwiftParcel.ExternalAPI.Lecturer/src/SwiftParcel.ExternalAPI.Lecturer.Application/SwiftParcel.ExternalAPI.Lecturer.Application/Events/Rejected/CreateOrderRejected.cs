using Convey.CQRS.Events;

namespace SwiftParcel.ExternalAPI.Lecturer.Application.Events.Rejected
{
    public class CreateOrderRejected : IRejectedEvent
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public Guid ParcelId { get; }
        public string Reason { get; }
        public string Code { get; }

        public CreateOrderRejected(Guid orderId, Guid customerId, Guid parcelId, string reason, string code)
        {
            OrderId = orderId;
            CustomerId = customerId;
            ParcelId = parcelId;
            Reason = reason;
            Code = code;
        }
    }
}