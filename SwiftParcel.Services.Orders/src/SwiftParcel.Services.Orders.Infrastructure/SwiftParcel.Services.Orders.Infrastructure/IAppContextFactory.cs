using SwiftParcel.Services.Orders.Application;

namespace SwiftParcel.Services.Orders.Infrastructure
{
    public interface IAppContextFactory
    {
        IAppContext Create();
    }
}

