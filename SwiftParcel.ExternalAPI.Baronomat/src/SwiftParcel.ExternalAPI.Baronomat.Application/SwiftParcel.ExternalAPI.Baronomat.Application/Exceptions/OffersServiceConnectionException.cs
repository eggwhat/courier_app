namespace SwiftParcel.ExternalAPI.Baronomat.Application.Exceptions
{
    public class OffersServiceConnectionException : AppException
    {
        public override string Code { get; } = "offers_service_connection_error";
        public OffersServiceConnectionException() : base("Offers service connection error.")
        {
        }
    }
}