using Convey.CQRS.Queries;
using SwiftParcel.Services.Parcels.Application.DTO;

namespace SwiftParcel.Services.Parcels.Application.Queries
{
    public class GetOffers : IQuery<IEnumerable<ExpirationStatusDto>>
    {
        public Guid ParcelId { get; set; }
    }
}
