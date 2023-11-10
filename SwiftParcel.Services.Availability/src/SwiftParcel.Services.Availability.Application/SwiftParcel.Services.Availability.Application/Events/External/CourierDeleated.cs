using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Availability.Application.Events.External
{
    public class CourierDeleated : IEvent
    {
        public Guid CourierId { get; }

        public CourierDeleated(Guid courierId) 
            => CourierId = courierId;
    }
}