using Convey.CQRS.Commands; 

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class CancelOrder : ICommand
    {
        public Guid OrderId { get; }

        public CancelOrder(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}