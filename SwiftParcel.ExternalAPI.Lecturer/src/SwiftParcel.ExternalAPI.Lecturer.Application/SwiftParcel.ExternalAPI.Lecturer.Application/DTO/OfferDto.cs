namespace SwiftParcel.ExternalAPI.Lecturer.Application.DTO
{
    public class OfferDto
    {
        public Guid OfferId { get; set; }
        public DimensionDto Dimensions { get; set; }
        public InquiryAddressDto Source { get; set; }
        public InquiryAddressDto Destination { get; set; }
        public double Weight { get; set; }
        public string WeightUnit { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime ValidTo { get; set; }
        public bool DeliveryInWeekend { get; set; }
        public string Priority { get; set; }
        public bool VipPackage { get; set; }
        public List<PriceBreakDownItemDto> PriceBreakDown { get; set; }
        public double TotalPrice { get; set; }
        public string Currency { get; set; }
        public DateTime InquireDate { get; set; }
        public DateTime OfferRequestDate { get; set; }
        public DateTime DecisionDate { get; set; }
        public string OfferStatus { get; set; }
        public string BuyerName { get; set; }
        public InquiryAddressDto BuyerAddress { get; set; }
    }
}