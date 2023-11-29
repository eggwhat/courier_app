using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class ApproveOrder: ICommand
    {
        public Guid OrderId { get; }
        public ApproveOrder(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}


