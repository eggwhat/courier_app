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
        private readonly string _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI4ZGU1N2M0My1jYmExLTQ0MWYtYTAyMi00Mjc5NDEyNDY5ZDgiLCJzdWIiOiJzd2lmdHBhcmNlbDIwMjRAZ21haWwuY29tIiwiZW1haWwiOiJzd2lmdHBhcmNlbDIwMjRAZ21haWwuY29tIiwianRpIjoiZGYxM2EzZGQtYjhjYy00ZTYwLWI1ZmEtODkyMWFjYTU3ZGZjIiwiaWF0IjoxNzA2MDQxMDAwLCJuYmYiOjE3MDYwNDEwMDAsImV4cCI6MTcwODcxOTQwMH0.J5vrF1pLq_Zoz4EJhCzm1nJCKkac0Aqco3Wt5g7qlHQ";
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