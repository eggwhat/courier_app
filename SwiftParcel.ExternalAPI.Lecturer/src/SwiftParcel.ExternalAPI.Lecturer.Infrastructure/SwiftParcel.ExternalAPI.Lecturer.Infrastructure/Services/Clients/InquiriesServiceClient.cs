using Convey.HTTP;
using System.Text;
using SwiftParcel.ExternalAPI.Lecturer.Application.DTO;
using SwiftParcel.ExternalAPI.Lecturer.Application.Services.Clients;

namespace SwiftParcel.ExternalAPI.Lecturer.Infrastructure.Services.Clients
{
    public class InquiriesServiceClient: IInquiriesServiceClient
    {
        private readonly IHttpClient _httpClient;
        private readonly string _url;
        public InquiriesServiceClient(IHttpClient httpClient, HttpClientOptions options)
        {
            _httpClient = httpClient;
            _url = options.Services["lecture-api"];
        }

        public Task<HttpResult<InquiryResponseDto>> PostAsync(string token, InquiryDto inquiry)
        {
            _httpClient.SetHeaders(new Dictionary<string, string>
            {
                { "Authorization", "Bearer " + token },
                { "Content-Type", "application/json"}
            });
            
            var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(inquiry);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            return _httpClient.PostAsync<HttpResult<InquiryResponseDto>>(_url + "/inquiries", content);
        }
    }
}