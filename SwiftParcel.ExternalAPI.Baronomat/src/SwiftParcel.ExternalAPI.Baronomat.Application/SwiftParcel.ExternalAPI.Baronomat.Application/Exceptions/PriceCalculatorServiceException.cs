namespace SwiftParcel.ExternalAPI.Baronomat.Application.Exceptions
{
    public class PriceCalculatorServiceException : AppException
    {
        public override string Code { get; } = "pricing_calculator_service_connection_error";
        public string ReasonPhrase { get; }
        public PriceCalculatorServiceException(string reasonPhrase) : base("Pricing calculator service connection error.")
        {
            ReasonPhrase = reasonPhrase;
        }
    }
}