namespace SwiftParcel.ExternalAPI.Baronomat.Application.Exceptions
{
    public class PriceCalculatorServiceConnectionException : AppException
    {
        public override string Code { get; } = "pricing_calculator_service_connection_error";
        public PriceCalculatorServiceConnectionException() : base("Pricing calculator service connection error.")
        {
        }
    }
}