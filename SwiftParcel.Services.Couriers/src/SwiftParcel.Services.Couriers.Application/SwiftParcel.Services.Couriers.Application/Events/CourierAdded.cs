using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Couriers.Application.Events
{
    public class CourierAdded : IEvent
    {
        public Guid CourierId { get; }

        public CourierAdded(Guid id)
            => CourierId = id;
    }
}