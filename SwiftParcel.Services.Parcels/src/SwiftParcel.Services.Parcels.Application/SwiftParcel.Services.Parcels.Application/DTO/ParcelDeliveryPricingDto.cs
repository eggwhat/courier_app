namespace SwiftParcel.Services.Parcels.Application.DTO
{
    public class ParcelDeliveryPricingDto
    {
        public Guid CustomerId { get; set; }
        public decimal OrderPrice { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public bool HighPriority { get; set; }
        public bool DeliverAtWeekend { get; set; }
        public bool VipPackage { get; set; }
    }
}