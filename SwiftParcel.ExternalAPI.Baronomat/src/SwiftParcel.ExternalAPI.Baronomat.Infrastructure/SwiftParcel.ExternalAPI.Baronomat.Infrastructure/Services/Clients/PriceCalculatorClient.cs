using Convey.HTTP;
using System.Text;
using SwiftParcel.ExternalAPI.Baronomat.Application.DTO;
using SwiftParcel.ExternalAPI.Baronomat.Application.Services.Clients;

namespace SwiftParcel.ExternalAPI.Baronomat.Infrastructure.Services.Clients
{
    public class PriceCalculatorClient: IPriceCalculatorClient
    {
        private readonly IHttpClient _httpClient;
        private readonly string _url;
        public PriceCalculatorClient(IHttpClient httpClient, HttpClientOptions options)
        {
            _httpClient = httpClient;
            _url = options.Services["baronomat-api"];
        }

        public Task<HttpResult<PriceResponseDto>> PostAsync(PriceRequestDto priceRequest)
        {
            _httpClient.SetHeaders(new Dictionary<string, string>
            {
                { "Authorization", "Bearer " + _token },
                { "Content-Type", "application/json"}
            });

            var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(priceRequest);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            return _httpClient.PostResultAsync<PriceResponseDto>(_url + "/PriceCalculator", content);
        }
    }
}