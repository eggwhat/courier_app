using SwiftParcel.Services.Deliveries.Application;

namespace SwiftParcel.Services.Deliveries.Infrastructure
{
    public interface IAppContextFactory
    {
        IAppContext Create();
    }
}