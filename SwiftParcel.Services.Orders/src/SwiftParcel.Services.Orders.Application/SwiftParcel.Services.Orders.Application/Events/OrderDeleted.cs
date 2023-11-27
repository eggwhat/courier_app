using System;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Orders.Application.Events
{
    public class OrderDeleted : IEvent
    {
        public Guid OrderId { get; }
        public OrderDeleted(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}