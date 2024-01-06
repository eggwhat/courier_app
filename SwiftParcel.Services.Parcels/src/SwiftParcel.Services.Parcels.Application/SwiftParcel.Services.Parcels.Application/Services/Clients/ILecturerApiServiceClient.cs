using SwiftParcel.Services.Parcels.Application.Commands;
using SwiftParcel.Services.Parcels.Application.DTO;

namespace SwiftParcel.Services.Parcels.Application.Services.Clients
{
    public interface ILecturerApiServiceClient
    {
        public Task PostInquiryAsync(AddParcel parcel);
        public Task<ExpirationStatusDto> GetOfferAsync(Guid parcelId);
    }
}   