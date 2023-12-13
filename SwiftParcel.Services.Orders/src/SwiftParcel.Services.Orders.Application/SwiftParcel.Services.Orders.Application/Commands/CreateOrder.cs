using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class CreateOrder: ICommand
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public CreateOrder(Guid orderId, Guid customerId)
        {
            OrderId = orderId == Guid.Empty ? Guid.NewGuid() : orderId;
            CustomerId = customerId;
        }
    }
}


