using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class CancelOrderOfficeWorker: ICommand
    {
        public Guid OrderId { get; }
        public string Reason { get; }
        public CancelOrderOfficeWorker(Guid orderId, string reason)
        {
            OrderId = orderId;
            Reason = reason;
        }
    }
}


