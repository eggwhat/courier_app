namespace SwiftParcel.ExternalAPI.Lecturer.Application.Exceptions
{
    public class InquiresServiceConnectionException : AppException
    {
        public override string Code { get; } = "inquires_service_connection_error";
        public InquiresServiceConnectionException() : base("Inquires service connection error.")
        {
        }
    }
}