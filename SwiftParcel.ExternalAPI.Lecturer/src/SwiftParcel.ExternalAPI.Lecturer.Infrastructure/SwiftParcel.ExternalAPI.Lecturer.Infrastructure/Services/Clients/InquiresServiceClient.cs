using Convey.HTTP;
using System.Text;
using SwiftParcel.ExternalAPI.Lecturer.Application.DTO;
using SwiftParcel.ExternalAPI.Lecturer.Application.Services.Clients;

namespace SwiftParcel.ExternalAPI.Lecturer.Infrastructure.Services.Clients
{
    public class InquiresServiceClient: IInquiresServiceClient
    {
        private readonly IHttpClient _httpClient;
        private readonly string _url;
        public InquiresServiceClient(IHttpClient httpClient, HttpClientOptions options)
        {
            _httpClient = httpClient;
            _url = options.Services["lecturer-api"];
        }

        public Task<HttpResult<InquiryResponseDto>> PostAsync(string token, InquiryDto inquiry)
        {
            _httpClient.SetHeaders(new Dictionary<string, string>
            {
                { "accept", "application.json"},
                { "Authorization", "Bearer " + token },
                { "Content-Type", "application/json"}
            });

            var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(inquiry);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            return _httpClient.PostResultAsync<InquiryResponseDto>(_url + "/Inquires", content);
        }
    }
}