using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Availability.Application.Commands
{
    public class DeleteResource: ICommand
    {
        public Guid ResourceId { get; }

        public DeleteResource(Guid resourceId)
            => ResourceId = resourceId;
    }
}