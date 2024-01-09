using System;
using System.Threading.Tasks;
using Convey.HTTP;
using SwiftParcel.Services.Parcels.Application.DTO;
using SwiftParcel.Services.Parcels.Application.Services.Clients;

namespace SwiftParcel.Services.Parcels.Infrastructure.Services.Clients
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

        public Task<ParcelDeliveryPricingDto> GetParcelDeliveryPriceAsync(Guid customerId, decimal orderPrice,
         double length, double width, double height, double weight, bool highPriority, bool deliverAtWeekend,
         bool vipPackage)
            => _httpClient.GetAsync<ParcelDeliveryPricingDto>($"{_url}/pricing?customerId={customerId}&orderPrice={orderPrice}&length={length}&width={width}&height={height}&weight={weight}&highPriority={highPriority.ToString().ToLowerInvariant()}&deliverAtWeekend={deliverAtWeekend.ToString().ToLowerInvariant()}&vipPackage={vipPackage.ToString().ToLowerInvariant()}");
    }
}