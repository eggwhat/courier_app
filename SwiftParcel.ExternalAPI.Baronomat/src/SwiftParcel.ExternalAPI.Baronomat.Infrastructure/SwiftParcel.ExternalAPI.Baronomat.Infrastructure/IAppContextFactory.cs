using SwiftParcel.ExternalAPI.Baronomat.Application;

namespace SwiftParcel.ExternalAPI.Baronomat.Infrastructure
{
    public interface IAppContextFactory
    {
        IAppContext Create();
    }
}