using Convey.Types;
using SwiftParcel.Services.Deliveries.Core.Entities;

namespace SwiftParcel.Services.Deliveries.Infrastructure.Mongo.Documents
{
    public class DeliveryDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid? CourierId { get; set; }
        public DeliveryStatus Status { get; set; }
        public double Volume { get; set; }
        public double Weight { get; set; }
        public Address Source { get; set; }
        public Address Destination { get; set; }
        public Priority Priority { get; set; }
        public bool AtWeekend { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime? DeliveryAttemptDate { get; set; }
        public string CannotDeliverReason { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}