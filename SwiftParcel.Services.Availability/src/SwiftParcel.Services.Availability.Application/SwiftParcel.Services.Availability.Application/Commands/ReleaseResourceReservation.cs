using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Availability.Application.Commands
{
    public class ReleaseResourceReservation: ICommand
    {
        public Guid ResourceId { get; }
        public DateTime DateTime { get; }

        public ReleaseResourceReservation(Guid resourceId, DateTime dateTime)
            => (ResourceId, DateTime) = (resourceId, dateTime);
    }
}