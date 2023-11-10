using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Availability.Application.Events
{
    public class ResourceReserved  : IEvent
    {
        public Guid ResourceId { get; }
        public DateTime DateTime { get; }

        public ResourceReserved(Guid resourceId, DateTime dateTime)
            => (ResourceId, DateTime) = (resourceId, dateTime);
    }
}