namespace SwiftParcel.ExternalAPI.Lecturer.Application
{
    public interface IAppContext
    {
        string RequestId { get; }
        IIdentityContext Identity { get; }
    }
}