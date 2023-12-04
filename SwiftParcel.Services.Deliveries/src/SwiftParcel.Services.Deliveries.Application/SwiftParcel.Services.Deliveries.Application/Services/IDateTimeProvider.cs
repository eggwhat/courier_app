namespace SwiftParcel.Services.Deliveries.Application.Services
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}