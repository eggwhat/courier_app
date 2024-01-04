namespace SwiftParcel.ExternalAPI.Lecturer.Application.Services
{
    public interface ITokenManager
    {
        string GetToken();
        void ValidateToken();
    }
}