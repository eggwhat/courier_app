using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class ConfirmOrder: ICommand
    {
        public Guid OrderId { get; }
        public ConfirmOrder(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}