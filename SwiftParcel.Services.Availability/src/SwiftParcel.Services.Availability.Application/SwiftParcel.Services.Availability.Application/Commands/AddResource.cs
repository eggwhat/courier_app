using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Availability.Application.Commands
{
    public class AddResource : ICommand
    {
        public Guid ResourceId { get; }
        public IEnumerable<string> Tags { get; }

        public AddResource(Guid resourceId, IEnumerable<string> tags)
            => (ResourceId, Tags) = (resourceId == Guid.Empty ? Guid.NewGuid() : resourceId,
                tags ?? Enumerable.Empty<string>());
    }
}