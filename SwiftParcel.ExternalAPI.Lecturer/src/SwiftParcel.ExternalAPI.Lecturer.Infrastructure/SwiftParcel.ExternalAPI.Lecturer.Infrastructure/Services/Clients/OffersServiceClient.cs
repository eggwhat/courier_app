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

        public Task<HttpResult<OfferResponseDto>> PostAsync(string token, OfferDto offer)
        {
            _httpClient.SetHeaders(new Dictionary<string, string>
            {
                { "accept", "application.json"},
                { "Authorization", "Bearer " + token },
                { "Content-Type", "application/json"}
            });

            var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(offer);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            return _httpClient.PostResultAsync<OfferResponseDto>(_url + "/Offers", content);
        }

        
    }
}