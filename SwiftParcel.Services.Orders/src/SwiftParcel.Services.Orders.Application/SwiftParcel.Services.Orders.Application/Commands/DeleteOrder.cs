using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class DeleteOrder: ICommand
    {
        public Guid OrderId { get; }
        public DeleteOrder(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}


