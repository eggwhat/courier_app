using SwiftParcel.ExternalAPI.Lecturer.Application.DTO;
using Convey.HTTP;

namespace SwiftParcel.ExternalAPI.Lecturer.Application.Services.Clients
{
    public interface IIdentityManagerServiceClient
    {
        Task<string> GetToken();
        Task ValidateToken();
    }   
}