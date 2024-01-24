using Convey.HTTP;
using SwiftParcel.ExternalAPI.Baronomat.Application.DTO;

namespace SwiftParcel.ExternalAPI.Baronomat.Application.Services.Clients
{
    public interface IInquiresServiceClient
    {
        Task<HttpResult<InquiryResponseDto>> PostAsync(string token, InquiryDto inquiry);
    }
}
