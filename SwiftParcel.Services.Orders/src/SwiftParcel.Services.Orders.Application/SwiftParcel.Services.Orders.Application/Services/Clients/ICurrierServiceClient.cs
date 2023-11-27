using SwiftParcel.Services.Orders.Application.DTO;

namespace SwiftParcel.Services.Orders.Application.Services.Clients
{
    public interface ICurrierServiceClient
    {
        Task<CurrierDto> GetAsync(Guid id);
    }
}