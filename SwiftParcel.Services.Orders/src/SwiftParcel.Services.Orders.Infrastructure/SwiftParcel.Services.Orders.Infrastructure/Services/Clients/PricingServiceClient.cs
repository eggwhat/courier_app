using System;
using System.Threading.Tasks;
using Convey.HTTP;
using SwiftParcel.Services.Orders.Application.DTO;
using SwiftParcel.Services.Orders.Application.Services.Clients;

namespace SwiftParcel.Services.Orders.Infrastructure.Services.Clients
{
    public class PricingServiceClient : IPricingServiceClient
    {
        private readonly IHttpClient _httpClient;
        private readonly string _url;

        public PricingServiceClient(IHttpClient httpClient, HttpClientOptions options)
        {
            _httpClient = httpClient;
            _url = options.Services["pricing"];
        }

        public Task<OrderPricingDto> GetOrderPriceAsync(Guid customerId, decimal orderPrice)
            => _httpClient.GetAsync<OrderPricingDto>($"{_url}/pricing?customerId={customerId}&orderPrice={orderPrice}");
    }
}