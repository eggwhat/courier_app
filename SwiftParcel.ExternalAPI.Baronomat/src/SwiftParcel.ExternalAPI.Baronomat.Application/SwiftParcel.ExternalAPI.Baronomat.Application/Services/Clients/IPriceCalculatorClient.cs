using Convey.HTTP;
using SwiftParcel.ExternalAPI.Baronomat.Application.DTO;

namespace SwiftParcel.ExternalAPI.Baronomat.Application.Services.Clients
{
    public interface IPriceCalculatorClient
    {
        Task<HttpResult<PriceResponseDto>> PostAsync(PriceRequestDto priceRequest);
    }
}