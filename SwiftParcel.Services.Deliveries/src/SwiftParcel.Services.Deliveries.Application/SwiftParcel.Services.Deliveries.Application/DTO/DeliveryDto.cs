using SwiftParcel.Services.Deliveries.Core.Entities;

namespace SwiftParcel.Services.Deliveries.Application.DTO
{
    public class DeliveryDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid? CourierId { get; set; }
        public string Status { get; set; }
        public double Volume { get; set; }
        public double Weight { get; set; }
        public AddressDto Source { get; set; }
        public AddressDto Destination { get; set; }
        public string Priority { get; set; }
        public bool AtWeekend { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime? DeliveryAttemptDate { get; set; }
        public string CannotDeliverReason { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}