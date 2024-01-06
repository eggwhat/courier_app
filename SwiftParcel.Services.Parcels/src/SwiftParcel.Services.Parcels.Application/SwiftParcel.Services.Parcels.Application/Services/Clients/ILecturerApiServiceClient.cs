using SwiftParcel.Services.Parcels.Application.Commands;

namespace SwiftParcel.Services.Parcels.Application.Services.Clients
{
    public interface ILecturerApiServiceClient
    {
        public Task PostInquiryAsync(AddParcel parcel);
    }
}   