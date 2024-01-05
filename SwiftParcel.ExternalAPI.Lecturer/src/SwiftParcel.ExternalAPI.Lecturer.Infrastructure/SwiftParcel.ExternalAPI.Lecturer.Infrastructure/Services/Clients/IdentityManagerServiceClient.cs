using System.Text;
using Convey.HTTP;
using SwiftParcel.ExternalAPI.Lecturer.Application.DTO;
using SwiftParcel.ExternalAPI.Lecturer.Application.Services.Clients;

namespace SwiftParcel.ExternalAPI.Lecturer.Infrastructure.Services.Clients
{
    public class IdentityManagerServiceClient : IIdentityManagerServiceClient
    {
        private readonly IHttpClient _httpClient;
        private readonly string _url;
        private readonly string postData = "grant_type=client_credentials";
        private readonly StringContent _content;
        private string AccessToken { get; set; }
        private DateTime ValidTo { get; set; }

        //this should be replaced by some cloud key vault
        private readonly string _token = "dGVhbTJkOkVBQUE1MEI4LTkwQ0ItNDM2RS05ODY0LTRCQzc1QjU2RjNCRQ==";
        
        public IdentityManagerServiceClient(IHttpClient httpClient, HttpClientOptions options)
        {
            _httpClient = httpClient;
            _url = options.Services["lecturer-api-identity"];
            var headers = new Dictionary<string, string>
            {
                { "Authorization", "Basic " + _token },
                { "Content-Type", "application/x-www-form-urlencoded" }
            };
            _httpClient.SetHeaders(headers);
            _content = new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded");
        }

        private async Task GenerateToken()
        {
            var response =  await _httpClient.PostResultAsync<AccessTokenDto>($"{_url}", _content);
            AccessToken = response.Result.Access_Token;
            ValidTo = DateTime.Now.AddSeconds(response.Result.Expires_In - 300);
        }

        public async Task<string> GetToken()
        {
            await ValidateToken();
            return AccessToken;
        }
        
        public async Task ValidateToken()
        {
            if (DateTime.Now >= ValidTo)
            {
                await GenerateToken();
            }
        }
    }
}