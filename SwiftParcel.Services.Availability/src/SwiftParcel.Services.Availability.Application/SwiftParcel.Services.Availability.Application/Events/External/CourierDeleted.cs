using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace SwiftParcel.Services.Availability.Application.Events.External
{
    [Message("couriers")]
    public class CourierDeleted : IEvent
    {
        public Guid CourierId { get; }

        public CourierDeleted(Guid courierId) 
            => CourierId = courierId;
    }
}