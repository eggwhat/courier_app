namespace SwiftParcel.ExternalAPI.Lecturer.Application.DTO
{
    public class PriceBreakDownDto
    {
        public PriceDetailsDto BasePrice { get; set; }
        public PriceDetailsDto DimensionSurcharge { get; set; }
        public PriceDetailsDto PickupAndDelivaryDate { get; set; }
        public PriceDetailsDto VipPackage { get; set; }
    }

    public class PriceDetailsDto
    {
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
    }
}
