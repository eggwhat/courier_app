namespace SwiftParcel.Services.Orders.Application.Exceptions
{
    public class LecturerApiServiceConnectionException: AppException
    {
        public override string Code { get; } = "lecturer_api_service_connection_error";
        public LecturerApiServiceConnectionException(): base("Lecturer api service connection error.")
        {
        }
    }
}
