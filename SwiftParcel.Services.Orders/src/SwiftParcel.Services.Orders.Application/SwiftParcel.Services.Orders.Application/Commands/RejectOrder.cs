using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class RejectOrder: ICommand
    {
        public Guid OrderId { get; }
        public string Reason { get; }
        public RejectOrder(Guid orderId, string reason)
        {
            OrderId = orderId;
            Reason = reason;
        }
    }
}


