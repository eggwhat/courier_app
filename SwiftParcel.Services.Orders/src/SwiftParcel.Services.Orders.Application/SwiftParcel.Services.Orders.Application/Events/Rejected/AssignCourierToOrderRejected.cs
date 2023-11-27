using Convey.CQRS.Events;

namespace SwiftParcel.Services.Orders.Application.Events.Rejected
{
    public class AssignCourierToOrderRejected : IRejectedEvent
    {
        public Guid OrderId { get; }
        public Guid CourierId { get; }
        public string Reason { get; }
        public string Code { get; }

        public AssignCourierToOrderRejected(Guid orderId, Guid courierId, string reason, string code)
        {
            OrderId = orderId;
            CourierId = courierId;
            Reason = reason;
            Code = code;
        }
    }
}
