using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Couriers.Application.Events
{
    public class CourierUpdated : IEvent
    {
        public Guid CourierDeleated { get; }

        public CourierUpdated(Guid id)
            => CourierDeleated = id;
    }
}