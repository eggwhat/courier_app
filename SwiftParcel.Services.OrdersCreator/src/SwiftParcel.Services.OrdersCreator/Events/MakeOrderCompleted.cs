using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.OrdersCreator.Events
{
    public class MakeOrderCompleted : IEvent
    {
        public Guid OrderId { get; }

        public MakeOrderCompleted(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}