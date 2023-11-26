using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Couriers.Application.Events
{
    public class CourierUpdated : IEvent
    {
        public Guid CourierUpdated { get; }

        public CourierUpdated(Guid id)
            => CourierUpdated = id;
    }
}