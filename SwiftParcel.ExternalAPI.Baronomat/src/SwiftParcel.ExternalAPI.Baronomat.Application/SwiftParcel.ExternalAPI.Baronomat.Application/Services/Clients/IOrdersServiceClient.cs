using Convey.HTTP;
using SwiftParcel.ExternalAPI.Baronomat.Application.DTO;

namespace SwiftParcel.ExternalAPI.Baronomat.Application.Services.Clients
{
    public interface IOrdersServiceClient
    {
        Task<HttpResult<OrderResponseDto>> PostAsync(OrderRequestDto orderRequest);
        Task<HttpResult<OrderResponseDto>> GetOrderAsync(string orderId);
    }
}