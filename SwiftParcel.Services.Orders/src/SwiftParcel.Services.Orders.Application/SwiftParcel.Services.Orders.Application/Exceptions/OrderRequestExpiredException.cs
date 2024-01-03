namespace SwiftParcel.Services.Orders.Application.Exceptions
{
    public class OrderRequestExpiredException : AppException
    {
        public override string Code { get; } = "order_request_expired";
        public Guid OrderId { get; }
        public DateTime RequestValidTo { get; }

        public OrderRequestExpiredException(Guid orderId, DateTime requestValidTo)
            : base($"Order with id: {orderId} request expired at: {requestValidTo}.")
        {
            OrderId = orderId;
            RequestValidTo = requestValidTo;
        }
    }
}