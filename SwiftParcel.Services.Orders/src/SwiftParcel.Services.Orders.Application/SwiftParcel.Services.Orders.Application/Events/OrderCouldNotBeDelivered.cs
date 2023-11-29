using System;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Orders.Application.Events
{
    public class OrderCouldNotBeDelivered : IEvent
    {
        public Guid OrderId { get; }
        public string Reason { get; }
        public OrderCouldNotBeDelivered(Guid orderId, string reason)
        {
            OrderId = orderId;
            Reason = reason;
        }
    }
}