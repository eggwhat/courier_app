namespace SwiftParcel.Services.Orders.Core.Exceptions
{
    public class InvalidOrderPriceException : DomainException
    {
        public override string Code { get; } = "invalid_order_price";
        public Guid Id { get; }
        public decimal Price { get; }

        public InvalidOrderPriceException(Guid id, decimal price) : base($"Order with id: {id} has invalid price: {price}.")
        {
            Id = id;
            Price = price;
        }
    }
}
