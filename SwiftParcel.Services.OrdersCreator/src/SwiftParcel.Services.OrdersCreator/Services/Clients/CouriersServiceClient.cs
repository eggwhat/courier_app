using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.HTTP;
using SwiftParcel.Services.OrdersCreator.DTO;

namespace SwiftParcel.Services.OrdersCreator.Services.Clients
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

        public async Task<CourierDto> GetBestAsync()
        {
            // the most potential resource of the issues to resolve in the api connection
            var couriers = await _httpClient.GetAsync<PagedResult<CourierDto>>($"{_url}/courier");
            var bestCourier = vehicles?.Items?.FirstOrDefault();
            if (bestCourier is null)
            {
                throw new InvalidOperationException("The best vehicle was not found.");
            }

            return bestCourier;
        }
    }
}