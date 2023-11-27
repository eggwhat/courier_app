using SwiftParcel.Services.Orders.Application.DTO;

namespace SwiftParcel.Services.Orders.Application.Services.Clients
{
    public interface IParcelServiceClient
    {
        Task<ParcelDto> GetAsync(Guid id);
    }
}

