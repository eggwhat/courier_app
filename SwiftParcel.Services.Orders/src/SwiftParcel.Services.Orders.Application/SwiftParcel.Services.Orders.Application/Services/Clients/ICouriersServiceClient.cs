using SwiftParcel.Services.Orders.Application.DTO;

namespace SwiftParcel.Services.Orders.Application.Services.Clients
{
    public interface ICouriersServiceClient
    {
        Task<CourierDTO> GetAsync(Guid id);
    }
}