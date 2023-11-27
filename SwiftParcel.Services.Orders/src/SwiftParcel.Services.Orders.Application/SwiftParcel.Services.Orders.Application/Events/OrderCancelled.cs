using System;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Orders.Application.Events
{
    public class OrderRejected : IEvent
    {
        public Guid OrderId { get; }
        public string Reason { get; }
        public OrderRejected(Guid orderId, string reason)
        {
            OrderId = orderId;
            Reason = reason;
        }
    }
}