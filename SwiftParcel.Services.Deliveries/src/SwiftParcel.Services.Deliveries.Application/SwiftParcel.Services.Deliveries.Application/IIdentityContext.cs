namespace SwiftParcel.Services.Deliveries.Application
{
    public interface IIdentityContext
    {
        Guid Id { get; }
        string Role { get; }
        bool IsAuthenticated { get; }
        bool IsOfficeWorker { get; }
        bool IsCourier { get; }
        IDictionary<string, string> Claims { get; }
    }
}