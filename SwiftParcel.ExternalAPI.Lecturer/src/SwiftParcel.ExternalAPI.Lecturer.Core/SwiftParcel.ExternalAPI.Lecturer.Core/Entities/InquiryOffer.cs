using SwiftParcel.ExternalAPI.Lecturer.Core.Entities;

namespace SwiftParcel.ExternalAPI.Lecturer.Core.Entities
{
    public class InquiryOffer
    {
        public Guid ParcelId { get; set; }
        public Guid InquiryId { get; set; }
        public double TotalPrice { get; set; }
        public DateTime ExpiringAt { get; set; }
        public List<PriceBreakDownItem> PriceBreakDown { get; set; }
        public InquiryOffer(Guid parcelId, Guid inquiryId, double totalPrice, DateTime expiringAt,
            List<PriceBreakDownItem> priceBreakDown)
        {
            ParcelId = parcelId;
            InquiryId = inquiryId;
            TotalPrice = totalPrice;
            ExpiringAt = expiringAt;
            PriceBreakDown = priceBreakDown;
        }
        
    }
}