namespace SwiftParcel.Services.Orders.Core.Exceptions
{
    public class InvalidBuyerEmailException : DomainException
    {
        public override string Code { get; } = "invalid_buyer_email";
        public string BuyerEmail { get; }

        public InvalidBuyerEmailException(string buyerEmail) : base($"Invalid buyer email: {buyerEmail}.")
        {
            BuyerEmail = buyerEmail;
        }
    }
}