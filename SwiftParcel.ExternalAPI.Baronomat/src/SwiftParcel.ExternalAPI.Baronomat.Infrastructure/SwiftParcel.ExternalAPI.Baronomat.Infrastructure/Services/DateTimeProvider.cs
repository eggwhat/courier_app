using SwiftParcel.ExternalAPI.Baronomat.Application.Services;

namespace SwiftParcel.ExternalAPI.Baronomat.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now  => DateTime.UtcNow;
    }
}