using System;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Orders.Application.Events
{
    public class CourierAssignedToOrder : IEvent
    {
        public Guid OrderId { get; }
        public Guid CourierId {get;}
        public CourierAssignedToOrder(Guid orderId, Guid courierId)
        {
            OrderId = orderId;
            CourierId = courierId;
        }
    }
}