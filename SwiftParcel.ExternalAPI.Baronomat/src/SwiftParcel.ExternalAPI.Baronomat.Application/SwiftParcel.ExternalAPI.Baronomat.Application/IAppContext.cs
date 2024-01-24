namespace SwiftParcel.ExternalAPI.Baronomat.Application
{
    public interface IAppContext
    {
        string RequestId { get; }
        IIdentityContext Identity { get; }
    }
}