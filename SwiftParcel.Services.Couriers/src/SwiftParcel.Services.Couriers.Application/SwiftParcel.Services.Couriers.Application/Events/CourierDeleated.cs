using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Couriers.Application.Events
{
    public class CourierDeleated : IEvent
    {
        public Guid CourierId { get; }

        public CourierDeleated(Guid id)
            => CourierId = id;
    }
}