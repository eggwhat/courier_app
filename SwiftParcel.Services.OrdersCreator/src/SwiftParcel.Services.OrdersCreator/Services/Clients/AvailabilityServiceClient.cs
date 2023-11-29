using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.HTTP;
using SwiftParcel.Services.OrdersCreator.DTO;

namespace SwiftParcel.Services.OrdersCreator.Services.Clients
{
    public class AvailabilityServiceClient : IAvailabilityServiceClient
    {
        private readonly IHttpClient _httpClient;
        private readonly string _url;

        public AvailabilityServiceClient(IHttpClient httpClient, HttpClientOptions options)
        {
            _httpClient = httpClient;
            _url = options.Services["availability"];
        }
            
        public Task<ResourceDto> GetResourceReservationsAsync(Guid resourceId)
            => _httpClient.GetAsync<ResourceDto>($"{_url}/resources/{resourceId}");
    }
}