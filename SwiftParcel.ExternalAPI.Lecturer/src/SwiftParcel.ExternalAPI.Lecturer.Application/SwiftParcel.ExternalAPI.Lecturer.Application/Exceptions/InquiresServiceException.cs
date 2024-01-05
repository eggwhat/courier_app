namespace SwiftParcel.ExternalAPI.Lecturer.Application.Exceptions
{
    public class InquiresServiceException : AppException
    {
        public override string Code { get; } = "inquires_service_connection_error";
        public string ReasonPhrase { get; }
        public InquiresServiceException(string reasonPhrase) : base("Inquires service connection error.")
        {
            ReasonPhrase = reasonPhrase;
        }
    }
}