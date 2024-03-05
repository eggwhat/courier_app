using SwiftParcel.Services.Parcels.Application.Commands;
using SwiftParcel.Services.Parcels.Application.DTO;
public interface IBaronomatApiServiceClient
    {
        public Task PostInquiryAsync(AddParcel parcel);
        public Task<ExpirationStatusDto> GetOfferAsync(Guid parcelId);
    }