using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class SendCancellationEmail: ICommand
    {
        public Guid CustomerId { get; }
        public Guid OrderId { get; }
        public string Reason { get; }
        public SendCancellationEmail(Guid orderId, Guid customerId, string reason)
        {
            OrderId = orderId;
            CustomerId = customerId;
            Reason = reason;
        }
    }
}