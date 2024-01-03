using SwiftParcel.ExternalAPI.Lecturer.Application;

namespace SwiftParcel.ExternalAPI.Lecturer.Infrastructure
{
    public interface IAppContextFactory
    {
        IAppContext Create();
    }
}