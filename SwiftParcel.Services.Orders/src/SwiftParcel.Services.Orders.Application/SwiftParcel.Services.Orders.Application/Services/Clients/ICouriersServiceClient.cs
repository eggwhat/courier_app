using SwiftParcel.Services.Orders.Application.DTO;

namespace SwiftParcel.Services.Orders.Application.Services.Clients
{
    public interface ICouriersServiceClient
    {
        Task<CourierDto> GetAsync(Guid id);
    }
}