using Convey.HTTP;
using System.Text;
using SwiftParcel.ExternalAPI.Lecturer.Application.Services.Clients;
using SwiftParcel.ExternalAPI.Lecturer.Application.DTO;

namespace SwiftParcel.ExternalAPI.Lecturer.Infrastructure.Services.Clients
{
    public class OffersServiceClient : IOffersServiceClient
    {
        private readonly IHttpClient _httpClient;
        private readonly string _url;

        public OffersServiceClient(IHttpClient httpClient, HttpClientOptions options)
        {
            _httpClient = httpClient;
            _url = options.Services["lecturer-api"];
        }

        public async Task<HttpResult<OfferResponseDto>> PostAsync(string token, OfferRequestDto offer)
        {
            _httpClient.SetHeaders(new Dictionary<string, string>
            {
                { "accept", "application/json"},
                { "Authorization", "Bearer " + token },
                { "Content-Type", "application/json"}
            });

            var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(offer);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            return await _httpClient.PostResultAsync<OfferResponseDto>(_url + "/Offers", content);
        }

        public async Task<HttpResult<OfferRequestStatusDto>> GetOfferRequestStatus(string token, string offerRequestId)
        {
            _httpClient.SetHeaders(new Dictionary<string, string>
            {
                { "Authorization", "Bearer " + token },
            });
            return await _httpClient.GetResultAsync<OfferRequestStatusDto>(_url + $"/offer/request/{offerRequestId}/status");
        }

        public async Task<HttpResult<OfferDto>> GetOfferAsync(string token, string offerId)
        {
            _httpClient.SetHeaders(new Dictionary<string, string>
            {
                { "Authorization", "Bearer " + token },
            });
            return await _httpClient.GetResultAsync<OfferDto>(_url + $"/offer/{offerId}");   
        }

        public async Task<HttpResponseMessage> PostConfirmOffer(string token, string offerId)
        {
            _httpClient.SetHeaders(new Dictionary<string, string>
            {
                { "accept", "*/*" },
                { "Authorization", "Bearer " + token },
            });
            return await _httpClient.PostAsync(_url + $"/offer/{offerId}/confirm");
        }

        public async Task<HttpResponseMessage> DeleteCancelOffer(string token, string offerId)
        {
            _httpClient.SetHeaders(new Dictionary<string, string>
            {
                { "accept", "*/*" },
                { "Authorization", "Bearer " + token },
            });
            return await _httpClient.DeleteAsync(_url + $"/offer/{offerId}/cancel");
        }
    }
}