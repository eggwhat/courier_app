using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.OrdersCreator.Events.External
{
    public class CourierAssignedToOrder : IEvent
    {
        public Guid OrderId { get; }
        public Guid CourierId { get; }

        public CourierAssignedToOrder(Guid orderId, Guid courierId)
        {
            OrderId = orderId;
            CourierId = courierId;
        }
    }
}