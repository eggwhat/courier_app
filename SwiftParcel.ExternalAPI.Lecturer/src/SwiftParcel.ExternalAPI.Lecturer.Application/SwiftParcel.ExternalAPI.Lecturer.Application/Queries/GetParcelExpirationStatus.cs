using Convey.CQRS.Queries;
using SwiftParcel.ExternalAPI.Lecturer.Application.DTO;

namespace SwiftParcel.ExternalAPI.Lecturer.Application.Queries
{
    public class GetParcelExpirationStatus : IQuery<ExpirationStatusDto>
    {
        public Guid ParcelId { get; set; }
    }
}