using System;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Orders.Application.Events
{
    public class OrderReceived : IEvent
    {
        public Guid OrderId { get; }
        public OrderReceived(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}