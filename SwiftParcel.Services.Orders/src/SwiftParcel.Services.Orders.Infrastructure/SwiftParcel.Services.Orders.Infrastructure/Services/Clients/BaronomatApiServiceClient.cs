using System.Text;
using Convey.HTTP;
using SwiftParcel.Services.Orders.Application.Commands;
using SwiftParcel.Services.Orders.Application.DTO;
using SwiftParcel.Services.Orders.Application.Services.Clients;

namespace SwiftParcel.Services.Orders.Infrastructure.Services.Clients
{
    public class BaronomatApiServiceClient : IBaronomatApiServiceClient
    {
        private readonly IHttpClient _httpClient;
        private readonly string _url;

        public BaronomatApiServiceClient(IHttpClient httpClient, HttpClientOptions options)
        {
            _httpClient = httpClient;
            _url = options.Services["baronomat-api"];
        }
        
        public async Task<HttpResponseMessage> PostOfferAsync(CreateOrderBaronomat order)
        {
            var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(order);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync($"{_url}/orders", order);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersAsync(string customerId)
        {
            return await _httpClient.GetAsync<IEnumerable<OrderDto>>($"{_url}/orders?customerId={customerId}");
        }
        
    }
}
