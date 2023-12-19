namespace SwiftParcel.Services.Orders.Core.Exceptions
{
    public class InvalidBuyerNameException : DomainException
    {
        public override string Code { get; } = "invalid_buyer_name";
        public string BuyerName { get; }

        public InvalidBuyerNameException(string buyerName) : base($"Invalid buyer name: {buyerName}.")
        {
            BuyerName = buyerName;
        }
    }
}