using System;
using Convey.CQRS.Events;
using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Application.Events
{
    public class OrderConfirmed : IEvent
    {
        public Guid OrderId { get; set; }
        public double Width { get; protected set; }
        public double Height { get; protected set; }
        public double Depth { get; protected set; }
        public double Weight { get; set; }
        public Address Source { get; set; }
        public Address Destination { get; set; }
        public Priority Priority { get; set; }
        public bool AtWeekend { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public OrderConfirmed(Guid orderId, double width, double height, double depth, double weight, Address source, 
            Address destination, Priority priority, bool atWeekend, DateTime pickupDate, DateTime deliveryDate)
        {
            OrderId = orderId;
            Width = width;
            Height = height;
            Depth = depth;
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