namespace SwiftParcel.Services.Orders.Application.Exceptions
{
    public class BaronomatApiServiceConnectionException: AppException
    {
        public override string Code { get; } = "baronomat_api_service_connection_error";
        public BaronomatApiServiceConnectionException(): base("Baronomat api service connection error.")
        {
        }
    }
}
