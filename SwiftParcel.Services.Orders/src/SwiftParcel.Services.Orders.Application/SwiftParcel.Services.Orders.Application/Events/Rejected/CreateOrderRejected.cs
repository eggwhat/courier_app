using Convey.CQRS.Events;

namespace SwiftParcel.Services.Orders.Application.Events.Rejected
{
    public class CreateOrderRejected : IRejectedEvent
    {
        public Guid CustomerId { get; }
        public string Reason { get; }
        public string Code { get; }

        public CreateOrderRejected(Guid customerId, string reason, string code)
        {
            CustomerId = customerId;
            Reason = reason;
            Code = code;
        }
    }
}