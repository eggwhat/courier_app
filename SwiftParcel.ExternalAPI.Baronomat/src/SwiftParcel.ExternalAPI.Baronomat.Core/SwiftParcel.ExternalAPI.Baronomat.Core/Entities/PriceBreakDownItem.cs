namespace SwiftParcel.ExternalAPI.Baronomat.Core.Entities
{
    public class PriceBreakDownItem
    {
        public double Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }

        public PriceBreakDownItem(double amount, string currency, string description)
        {
            Amount = amount;
            Currency = currency;
            Description = description;
        }
    }
}