using SwiftParcel.ExternalAPI.Baronomat.Core.Entities;

namespace SwiftParcel.ExternalAPI.Baronomat.Core.Entities
{
    public class InquiryOffer
    {
        public Guid ParcelId { get; set; }
        public double TotalPrice { get; set; }
        public DateTime ExpiringAt { get; set; }
        public List<PriceBreakDownItem> PriceBreakDown { get; set; }
        public InquiryOffer(Guid parcelId, double totalPrice, DateTime expiringAt,
            List<PriceBreakDownItem> priceBreakDown)
        {
            ParcelId = parcelId;
            TotalPrice = totalPrice;
            ExpiringAt = expiringAt;
            PriceBreakDown = priceBreakDown;
        }
    }
}