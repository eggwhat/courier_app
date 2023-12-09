using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class SendCancellationEmail: ICommand
    {
        public string Subject { get; }
        public string Body { get; }
        public Guid CustomerId { get; }
        public Guid OrderId { get; }
        public SendCancellationEmail(Guid orderId, Guid customerId)
        {
            Subject = "Order cancelled";
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}