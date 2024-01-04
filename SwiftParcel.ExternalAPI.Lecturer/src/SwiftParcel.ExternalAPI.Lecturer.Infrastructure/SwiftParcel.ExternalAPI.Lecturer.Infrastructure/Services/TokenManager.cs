using SwiftParcel.ExternalAPI.Lecturer.Application.Services.Clients;
using SwiftParcel.ExternalAPI.Lecturer.Application.Services;

namespace SwiftParcel.ExternalAPI.Lecturer.Infrastructure.Services
{
    public class TokenManager: ITokenManager
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IIdentityManagerServiceClient _identityManagerServiceClient;
        private string AccessToken { get; set; }
        private DateTime ValidTo { get; set; }
        public TokenManager(IDateTimeProvider dateTimeProvider, IIdentityManagerServiceClient identityManagerServiceClient)
        {
            _dateTimeProvider = dateTimeProvider;
            _identityManagerServiceClient = identityManagerServiceClient;
            GenerateToken();
        }

        public string GetToken()
        {
            ValidateToken();
            return AccessToken;
        }
        
        public void ValidateToken()
        {
            if (_dateTimeProvider.Now >= ValidTo)
            {
                GenerateToken();
            }
        }

        private async void GenerateToken()
        {
            var response = await _identityManagerServiceClient.PostAsync();
            AccessToken = response.Result.Access_Token;
            ValidTo = _dateTimeProvider.Now.AddSeconds(response.Result.Expires_In - 300);
        }
    }
}