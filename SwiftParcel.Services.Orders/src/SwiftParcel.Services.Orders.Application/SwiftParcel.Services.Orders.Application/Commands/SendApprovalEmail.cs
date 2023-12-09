using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class SendApprovalEmail: ICommand
    {
        public Guid CustomerId { get; }
        public Guid OrderId { get; }
        public SendApprovalEmail(Guid orderId, Guid customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}