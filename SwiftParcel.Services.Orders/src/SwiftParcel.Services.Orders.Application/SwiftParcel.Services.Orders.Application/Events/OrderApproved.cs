using System;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Orders.Application.Events
{
    public class OrderApproved : IEvent
    {
        public Guid OrderId { get; }
        public OrderApproved(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}