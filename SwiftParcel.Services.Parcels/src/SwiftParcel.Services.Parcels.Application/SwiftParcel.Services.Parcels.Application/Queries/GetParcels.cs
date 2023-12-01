using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using SwiftParcel.Services.Parcels.Application.DTO;

namespace SwiftParcel.Services.Parcels.Application.Queries
{
    public class GetParcels : IQuery<IEnumerable<ParcelDto>>
    {
        public Guid? CustomerId { get; set; }
    }
}
