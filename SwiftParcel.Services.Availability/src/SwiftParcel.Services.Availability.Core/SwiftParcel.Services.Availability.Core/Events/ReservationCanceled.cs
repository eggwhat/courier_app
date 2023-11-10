using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Availability.Core.Events
{
    public class ReservationCanceled : IDomainEvent
    {
        public Resource Resource { get; }
        public Reservation Reservation { get; }

        public ReservationCanceled(Resource resource, Reservation reservation)
            => (Resource, Reservation) = (resource, reservation);
    }
}