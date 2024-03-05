using Convey.CQRS.Queries;
using SwiftParcel.ExternalAPI.Baronomat.Application.DTO;

namespace SwiftParcel.ExternalAPI.Baronomat.Application.Queries
{
    public class GetParcelExpirationStatus : IQuery<ExpirationStatusDto>
    {
        public Guid ParcelId { get; set; }
    }
}