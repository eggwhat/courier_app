using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class SendCancellationEmail: ICommand
    {
        public Guid OrderId { get; }
        public string CustomerName { get; }
        public string CustomerEmail { get; }        
        public string Reason { get; }
        public SendCancellationEmail(Guid orderId, string customerName,
            string customerEmail, string reason)
        {
            OrderId = orderId;
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            Reason = reason;
        }
    }
}