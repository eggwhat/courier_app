using SwiftParcel.Services.Deliveries.Core.Entities;

namespace SwiftParcel.Services.Deliveries.Application.DTO
{
    public class DeliveryDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public DeliveryStatus Status { get; set; }
        public double Volume { get; set; }
        public double Weight { get; set; }
        public AddressDto Source { get; set; }
        public AddressDto Destination { get; set; }
        public string Priority { get; set; }
        public bool AtWeekend { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Notes { get; set; }
        public DateTime LastUpdate { get; set; }
    
        
    }
}