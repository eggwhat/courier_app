using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class CancelOrder: ICommand
    {
        public Guid OrderId { get; }
        public string Reason { get; }
        public CancelOrder(Guid orderId, string reason)
        {
            OrderId = orderId;
            Reason = reason;
        }
    }
}


