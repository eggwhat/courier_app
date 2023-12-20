using Convey.MessageBrokers;
using Convey.CQRS.Events;
using SwiftParcel.Services.Deliveries.Core.Entities;

namespace SwiftParcel.Services.Deliveries.Application.Events.External
{
    [Message("orders")]
    public class OrderApproved: IEvent
    {
        public Guid OrderId { get; set; }
        public double Volume { get; set; }
        public double Weight { get; set; }
        public Address Source { get; set; }
        public Address Destination { get; set; }
        public Priority Priority { get; set; }
        public bool AtWeekend { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public OrderApproved(Guid orderId, double volume, double weight, Address source, 
            Address destination, Priority priority, bool atWeekend, DateTime pickupDate, DateTime deliveryDate)
        {
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