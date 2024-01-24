namespace SwiftParcel.Services.Orders.Application.Exceptions
{
    public class BaronomatApiServiceException: AppException
    {
        public override string Code { get; } = "baronomat_api_service_error";
        public string ReasonPhrase { get; }
        public BaronomatApiServiceException(string reasonPhrase): base($"Baronomat api service error, reason: {reasonPhrase}.")
        {
            ReasonPhrase = reasonPhrase;
        }
    }
}