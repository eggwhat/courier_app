using System;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Orders.Application.Events
{
    public class OrderDelivered : IEvent
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public OrderDelivered(Guid orderId, Guid customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}