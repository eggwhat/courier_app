using SwiftParcel.Services.Orders.Application.DTO;

namespace SwiftParcel.Services.Orders.Application.Services.Clients
{
    public interface IParcelsServiceClient
    {
        Task<ParcelDto> GetAsync(Guid id);
    }
}

