using SwiftParcel.Services.Deliveries.Application.Services;

namespace SwiftParcel.Services.Deliveries.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now  => DateTime.UtcNow;
    }
}