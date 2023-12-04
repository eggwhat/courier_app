using SwiftParcel.Services.Orders.Application.Services;

namespace SwiftParcel.Services.Orders.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now  => DateTime.UtcNow;
    }
}