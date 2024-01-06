using Convey.HTTP;
using SwiftParcel.Services.Parcels.Application.Commands;
using SwiftParcel.Services.Parcels.Application.Services.Clients;
using SwiftParcel.Services.Parcels.Infrastructure.Mongo.Documents;

namespace SwiftParcel.Services.Parcels.Infrastructure.Services.Clients
{
    public class LecturerApiServiceClient : ILecturerApiServiceClient
    {
        private readonly IHttpClient _httpClient;
        private readonly string _url;

        public LecturerApiServiceClient(IHttpClient httpClient, HttpClientOptions options)
        {
            _httpClient = httpClient;
            _url = options.Services["lecturer-api"];
        }
        
        public async Task PostInquiryAsync(AddParcel parcel)
        {
            await _httpClient.PostAsync($"{_url}/parcels", parcel);
        }
        
    }
}
