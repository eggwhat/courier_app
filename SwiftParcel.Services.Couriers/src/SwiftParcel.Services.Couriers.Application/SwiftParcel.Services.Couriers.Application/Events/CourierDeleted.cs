using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Couriers.Application.Events
{
    public class CourierDeleted : IEvent
    {
        public Guid CourierId { get; }

        public CourierDeleted(Guid id)
            => CourierId = id;
    }
}