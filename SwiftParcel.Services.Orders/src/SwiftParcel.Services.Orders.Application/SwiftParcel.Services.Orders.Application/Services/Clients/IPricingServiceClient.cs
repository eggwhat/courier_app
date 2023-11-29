using SwiftParcel.Services.Orders.Application.DTO;

namespace SwiftParcel.Services.Orders.Application.Services.Clients
{
    public interface IPricingServiceClient
    {
        Task<OrderPricingDto> GetOrderPriceAsync(Guid customerId, decimal orderPrice);
    }
}