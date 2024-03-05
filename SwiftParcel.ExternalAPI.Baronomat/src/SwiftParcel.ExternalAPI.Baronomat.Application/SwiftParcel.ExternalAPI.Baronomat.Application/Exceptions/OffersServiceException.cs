namespace SwiftParcel.ExternalAPI.Baronomat.Application.Exceptions
{
    public class OffersServiceException : AppException
    {
        public override string Code { get; } = "offers_service_error";
        public string ReasonPhrase { get; }
        public OffersServiceException(string reasonPhrase) : base("Offers service error.")
        {
            ReasonPhrase = reasonPhrase;
        }
    }
}