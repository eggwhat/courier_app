using Convey.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Application.Events
{
    public class ParcelAdded : IEvent
    {
        public Guid ParcelId { get; }
        public ParcelAdded(Guid parcelId)
        {
            ParcelId = parcelId;
        }
    }
}
