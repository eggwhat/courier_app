    namespace SwiftParcel.ExternalAPI.Baronomat.Application.DTO
{
    public class ParcelDto
    {
        public Guid Id { get; set; }
        public Guid? CustomerId { get; set; }  
        public string Description { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
        public double Weight { get; set; }
        public AddressDto Source { get; set; }
        public AddressDto Destination { get; set; }
        public string Priority { get; set; }
        public bool AtWeekend { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool IsCompany { get; set; }
        public bool VipPackage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ValidTo { get; set; }
        public decimal CalculatedPrice { get; set; }
        public List<PriceBreakDownItemDto> PriceBreakDown { get; set; }

        public ParcelDto()
        {
        }

        
    }
}