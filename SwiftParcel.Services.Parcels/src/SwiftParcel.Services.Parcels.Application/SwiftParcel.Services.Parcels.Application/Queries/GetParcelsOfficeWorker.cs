using Convey.CQRS.Queries;
using SwiftParcel.Services.Parcels.Application.DTO;

namespace SwiftParcel.Services.Parcels.Application.Queries
{
    public class GetParcelsOfficeWorker : IQuery<IEnumerable<ParcelDto>>
    {
        public Guid? CustomerId { get; set; }
    }
}