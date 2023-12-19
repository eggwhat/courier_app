using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class AddCustomerToOrder : ICommand
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }

        public AddCustomerToOrder(Guid orderId, Guid customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}