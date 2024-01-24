using SwiftParcel.ExternalAPI.Baronomat.Application.DTO;
using Convey.HTTP;

namespace SwiftParcel.ExternalAPI.Baronomat.Application.Services.Clients
{
    public interface IIdentityManagerServiceClient
    {
        Task<string> GetToken();
        Task ValidateToken();
    }   
}