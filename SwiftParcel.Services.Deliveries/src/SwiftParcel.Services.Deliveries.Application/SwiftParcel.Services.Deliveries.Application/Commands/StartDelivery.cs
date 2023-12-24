using Convey.CQRS.Commands;
using SwiftParcel.Services.Deliveries.Core.Entities;

namespace SwiftParcel.Services.Deliveries.Application.Commands
{
    public class StartDelivery : ICommand
    {
        public Guid DeliveryId { get; }
        public Guid OrderId { get; }
        public double Volume { get; protected set; }
        public double Weight { get; protected set; }
        public Address Source { get; protected set; }
        public Address Destination { get; protected set; }
        public Priority Priority { get; protected set; }
        public bool AtWeekend { get; protected set; }
        public DateTime PickupDate { get; protected set; }
        public DateTime DeliveryDate { get; protected set; }

        public StartDelivery(Guid deliveryId, Guid orderId, double volume, double weight, Address source,
            Address destination, Priority priority, bool atWeekend, DateTime pickupDate, DateTime deliveryDate)
        {
            DeliveryId = deliveryId == Guid.Empty ? Guid.NewGuid() : deliveryId;
            OrderId = orderId;
            Volume = volume;
            Weight = weight;
            Source = source;
            Destination = destination;
            Priority = priority;
            AtWeekend = atWeekend;
            PickupDate = pickupDate;
            DeliveryDate = deliveryDate;
        }
    }
}