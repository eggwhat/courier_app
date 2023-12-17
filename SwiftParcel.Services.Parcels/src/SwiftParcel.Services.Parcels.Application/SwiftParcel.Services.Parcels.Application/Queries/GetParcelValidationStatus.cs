using Convey.CQRS.Queries;
using SwiftParcel.Services.Parcels.Application.DTO;

namespace SwiftParcel.Services.Parcels.Application.Queries
{
    public class GetParcelValidationStatus : IQuery<ValidationStatusDto>
    {
        public Guid ParcelId { get; set; }
    }
}
