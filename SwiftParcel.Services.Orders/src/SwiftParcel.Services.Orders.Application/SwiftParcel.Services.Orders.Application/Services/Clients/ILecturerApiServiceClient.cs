using Convey.HTTP;
using SwiftParcel.Services.Orders.Application.Commands;
using SwiftParcel.Services.Orders.Application.DTO;

namespace SwiftParcel.Services.Orders.Application.Services.Clients
{
    public interface ILecturerApiServiceClient
    {
        Task<HttpResult<object>> PostOfferAsync(CreateOrder order);
        Task<IEnumerable<OrderDto>> GetOrderRequestsAsync(string customerId);
        Task<IEnumerable<OrderDto>> GetOrdersAsync(string customerId);
        Task<HttpResult<object>> PostConfirmOrder(string orderId);
        Task<HttpResult<object>> PostCancelOrder(string orderId);
    }
}
