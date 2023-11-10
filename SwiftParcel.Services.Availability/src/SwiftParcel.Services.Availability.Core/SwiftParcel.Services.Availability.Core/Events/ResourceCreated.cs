using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Availability.Core.Events
{
    public class ResourceCreated : IDomainEvent
    {
        public Resource Resource { get; }

        public ResourceCreated(Resource resource)
            => Resource = resource;
    }
}