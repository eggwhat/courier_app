using System;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Orders.Application.Events
{
    public class OrderCancelled : IEvent
    {
        public Guid OrderId { get; }
        public string Reason { get; }
        public OrderCancelled(Guid orderId, string reason)
        {
            OrderId = orderId;
            Reason = reason;
        }
    }
}