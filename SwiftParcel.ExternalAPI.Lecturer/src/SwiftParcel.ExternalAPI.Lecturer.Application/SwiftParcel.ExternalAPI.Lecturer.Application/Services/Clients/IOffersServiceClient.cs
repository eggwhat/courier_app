using Convey.HTTP;
using SwiftParcel.ExternalAPI.Lecturer.Application.DTO;

namespace SwiftParcel.ExternalAPI.Lecturer.Application.Services.Clients
{
    public interface IOffersServiceClient
    {
        public Task<HttpResult<OfferResponseDto>> PostAsync(string token, OfferRequestDto offer);
        public Task<HttpResult<OfferRequestStatusDto>> GetOfferRequestStatus(string token, string offerRequestId);
        public Task<HttpResult<OfferDto>> GetOfferAsync(string token, string offerId);
        public Task<HttpResponseMessage> PostConfirmOffer(string token, string offerId);
        public Task<HttpResponseMessage> DeleteCancelOffer(string token, string offerId);
        
    }
}