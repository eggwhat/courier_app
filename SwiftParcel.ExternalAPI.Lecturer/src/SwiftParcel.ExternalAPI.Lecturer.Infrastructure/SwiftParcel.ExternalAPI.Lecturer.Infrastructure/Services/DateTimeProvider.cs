using SwiftParcel.ExternalAPI.Lecturer.Application.Services;

namespace SwiftParcel.ExternalAPI.Lecturer.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now  => DateTime.UtcNow;
    }
}