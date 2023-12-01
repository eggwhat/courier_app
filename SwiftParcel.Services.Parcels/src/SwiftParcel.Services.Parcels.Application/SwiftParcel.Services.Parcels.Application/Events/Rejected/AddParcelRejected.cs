using Convey.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Application.Events.Rejected
{
    public class AddParcelRejected : IRejectedEvent
    {
        public Guid ParcelId { get; }
        public string Reason { get; }
        public string Code { get; }

        public AddParcelRejected(Guid parcelId, string reason, string code)
        {
            ParcelId = parcelId;
            Reason = reason;
            Code = code;
        }
    }
}
