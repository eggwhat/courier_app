using SwiftParcel.Services.Orders.Application.DTO;
using SwiftParcel.Services.Orders.Application.Commands;

namespace SwiftParcel.Services.Orders.Application.Services.Clients
{
    public interface IBaronomatApiServiceClient
    {
        Task<HttpResponseMessage> PostOfferAsync(CreateOrderBaronomat order);
        Task<IEnumerable<OrderDto>> GetOrdersAsync(string customerId);
    }
}