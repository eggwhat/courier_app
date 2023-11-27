using System;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Orders.Application.Events
{
    public class OrderCreated : IEvent
    {
        public Guid OrderId { get; }
        public OrderCreated(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}