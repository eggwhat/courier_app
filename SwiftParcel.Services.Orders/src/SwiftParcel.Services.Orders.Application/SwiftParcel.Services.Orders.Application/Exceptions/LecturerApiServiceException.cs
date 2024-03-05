namespace SwiftParcel.Services.Orders.Application.Exceptions
{
    public class LecturerApiServiceException: AppException
    {
        public override string Code { get; } = "lecturer_api_service_error";
        public string ReasonPhrase { get; }
        public LecturerApiServiceException(string reasonPhrase): base($"Lecturer api service error, reason: {reasonPhrase}.")
        {
            ReasonPhrase = reasonPhrase;
        }
    }
}