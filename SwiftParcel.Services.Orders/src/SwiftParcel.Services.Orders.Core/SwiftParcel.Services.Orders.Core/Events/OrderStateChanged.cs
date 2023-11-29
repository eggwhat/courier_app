using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Core.Events
{
    public class OrderStateChanged : IDomainEvent
    {
        public Order Order { get; }

        public OrderStateChanged(Order order)
        {
            Order = order;
        }
    }
}
