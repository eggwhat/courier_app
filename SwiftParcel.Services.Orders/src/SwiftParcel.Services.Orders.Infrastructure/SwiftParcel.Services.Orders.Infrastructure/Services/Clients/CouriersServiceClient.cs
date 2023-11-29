using System;
using System.Threading.Tasks;
using Convey.HTTP;
using SwiftParcel.Services.Orders.Application.DTO;
using SwiftParcel.Services.Orders.Application.Services.Clients;

namespace SwiftParcel.Services.Orders.Infrastructure.Services.Clients
{
    public class CouriersServiceClient : ICouriersServiceClient
    {
        private readonly IHttpClient _httpClient;
        private readonly string _url;

        public CouriersServiceClient(IHttpClient httpClient, HttpClientOptions options)
        {
            _httpClient = httpClient;
            _url = options.Services["couriers"];
        }

        public Task<CourierDto> GetAsync(Guid id)
            => _httpClient.GetAsync<CourierDto>($"{_url}/couriers/{id}");
    }
}