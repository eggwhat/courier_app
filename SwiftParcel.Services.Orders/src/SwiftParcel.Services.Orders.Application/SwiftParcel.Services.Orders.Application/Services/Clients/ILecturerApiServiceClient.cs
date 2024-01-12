using Convey.HTTP;
using SwiftParcel.Services.Orders.Application.Commands;
using SwiftParcel.Services.Orders.Application.DTO;

namespace SwiftParcel.Services.Orders.Application.Services.Clients
{
    public interface ILecturerApiServiceClient
    {
        Task<HttpResponseMessage> PostOfferAsync(CreateOrderMiniCurrier order);
        Task<IEnumerable<OrderDto>> GetOrderRequestsAsync(string customerId);
        Task<IEnumerable<OrderDto>> GetOrdersAsync(string customerId);
        Task<HttpResponseMessage> PostConfirmOrder(string orderId);
        Task<HttpResponseMessage> PostCancelOrder(string orderId);
    }
}
