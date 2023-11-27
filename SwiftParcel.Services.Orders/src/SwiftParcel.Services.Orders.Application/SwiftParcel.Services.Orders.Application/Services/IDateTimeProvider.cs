namespace SwiftParcel.Services.Orders.Application.Services
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}