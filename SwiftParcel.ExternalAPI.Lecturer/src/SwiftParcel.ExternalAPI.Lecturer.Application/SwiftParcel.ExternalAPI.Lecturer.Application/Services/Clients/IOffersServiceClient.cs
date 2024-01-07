using Convey.HTTP;
using SwiftParcel.ExternalAPI.Lecturer.Application.DTO;

namespace SwiftParcel.ExternalAPI.Lecturer.Application.Services.Clients
{
    public interface IOffersServiceClient
    {
        public Task<HttpResult<OfferResponseDto>> PostAsync(string token, OfferRequestDto offer);
    }
}