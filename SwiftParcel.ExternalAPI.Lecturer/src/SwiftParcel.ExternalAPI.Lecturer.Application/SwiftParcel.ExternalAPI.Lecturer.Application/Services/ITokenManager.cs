namespace SwiftParcel.ExternalAPI.Lecturer.Application.Services
{
    public interface ITokenManager
    {
        string AccessToken { get; }
        DateTime ValidTo { get; }
        void ValidateToken();
    }
}