using Convey.HTTP;
using SwiftParcel.ExternalAPI.Lecturer.Application.DTO;

namespace SwiftParcel.ExternalAPI.Lecturer.Application.Services.Clients
{
    public interface IInquiriesServiceClient
    {
        Task<HttpResult<InquiryResponseDto>> PostAsync(string token, InquiryDto inquiry);
    }
}
