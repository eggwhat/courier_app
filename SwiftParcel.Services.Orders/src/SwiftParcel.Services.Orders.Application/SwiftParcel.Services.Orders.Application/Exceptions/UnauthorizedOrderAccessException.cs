using System;

namespace SwiftParcel.Services.Orders.Application.Exceptions
{
    public class UnauthorizedOrderAccessException : AppException
    {
        public override string Code { get; } = "unauthorized_order_access";
        public Guid OrderId { get; }
        public Guid CustomerId { get; }

        public UnauthorizedOrderAccessException(Guid orderId, Guid customerId)
            : base($"Unauthorized access to order with id: '{orderId}' by customer with id: '{customerId}'.")
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}