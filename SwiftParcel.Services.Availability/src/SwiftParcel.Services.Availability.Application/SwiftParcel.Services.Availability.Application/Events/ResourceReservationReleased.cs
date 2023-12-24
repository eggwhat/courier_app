using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Availability.Application.Events
{
    public class ResourceReservationReleased : IEvent
     {
        public Guid ResourceId { get; }
        public DateTime DateTime { get; }

        public ResourceReservationReleased(Guid resourceId, DateTime dateTime)
            => (ResourceId, DateTime) = (resourceId, dateTime);
    }
}