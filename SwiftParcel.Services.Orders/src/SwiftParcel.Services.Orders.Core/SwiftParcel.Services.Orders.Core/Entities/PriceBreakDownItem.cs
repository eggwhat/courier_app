namespace SwiftParcel.Services.Orders.Core.Entities
{
    public class PriceBreakDownItem
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }

        public PriceBreakDownItem(decimal amount, string currency, string description)
        {
            Amount = amount;
            Currency = currency;
            Description = description;
        }
    }
}
