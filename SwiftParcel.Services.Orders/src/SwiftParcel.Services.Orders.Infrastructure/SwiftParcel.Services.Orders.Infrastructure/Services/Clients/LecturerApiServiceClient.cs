using Convey.HTTP;
using SwiftParcel.Services.Orders.Application.Commands;
using SwiftParcel.Services.Orders.Application.DTO;
using SwiftParcel.Services.Orders.Application.Services.Clients;

namespace SwiftParcel.Services.Orders.Infrastructure.Services.Clients
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
        
        public async Task<HttpResponseMessage> PostOfferAsync(CreateOrderMiniCurrier order)
        {
            return await _httpClient.PostAsync($"{_url}/orders", order);
        }

        public async Task<IEnumerable<OrderDto>> GetOrderRequestsAsync(string customerId)
        {
            return await _httpClient.GetAsync<IEnumerable<OrderDto>>($"{_url}/orders/requests?customerId={customerId}");
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersAsync(string customerId)
        {
            return await _httpClient.GetAsync<IEnumerable<OrderDto>>($"{_url}/orders?customerId={customerId}");
        }

        public async Task<HttpResponseMessage> PostConfirmOrder(string orderId)
        {
            var content = new Dictionary<string, string>
            {
                {"orderId", orderId}
            };
            return await _httpClient.PostAsync($"{_url}/orders/{orderId}/confirm", content);
        }

        public async Task<HttpResponseMessage> PostCancelOrder(string orderId)
        {
            var content = new Dictionary<string, string>
            {
                {"orderId", orderId}
            };
            return await _httpClient.PostAsync($"{_url}/orders/{orderId}/cancel", content);
        }
        
    }
}
