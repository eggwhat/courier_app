namespace SwiftParcel.Services.Orders.Core.Exceptions
{
    public class CustomerAlreadyAddedToOrderException : DomainException
    {
        public override string Code { get; } = "customer_already_added_to_order";
        public Guid CustomerId { get; }
        public Guid OrderId { get; }

    
        public CustomerAlreadyAddedToOrderException(Guid customerId, Guid orderId)
            : base($"Customer with id: {customerId} was already added to order with id: {orderId}.")
        {
            CustomerId = customerId;
            OrderId = orderId;
        }
    }
}