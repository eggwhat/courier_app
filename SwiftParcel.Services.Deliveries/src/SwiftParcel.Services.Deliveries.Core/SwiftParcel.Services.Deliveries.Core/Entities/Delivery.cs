using SwiftParcel.Services.Deliveries.Core.Exceptions;
using SwiftParcel.Services.Deliveries.Core.Events;
using SwiftParcel.Services.Deliveries.Core.ValueObjects;

namespace SwiftParcel.Services.Deliveries.Core.Entities
{
    public class Delivery : AggregateRoot
    {
        public Guid OrderId { get; protected set; }
        public Guid? CourierId { get; protected set; }
        public DeliveryStatus Status { get; protected set; }
        public double Volume { get; protected set; }
        public double Weight { get; protected set; }
        public Address Source { get; protected set; }
        public Address Destination { get; protected set; }
        public Priority Priority { get; protected set; }
        public bool AtWeekend { get; protected set; }
        public DateTime PickupDate { get; protected set; }
        public DateTime DeliveryDate { get; protected set; }
        public string Notes { get; protected set; }
        public DateTime LastUpdate { get; protected set; }

        public Delivery(AggregateId id, Guid orderId, DateTime createdAt, DeliveryStatus status, 
            double volume, double weight, Address source, Address destination, Priority priority, 
            bool atWeekend, DateTime pickupDate, DateTime deliveryDate)
        {
            Id = id;
            OrderId = orderId;
            CourierId = null;
            Status = status;
            Volume = volume;
            Weight = weight;
            Source = source;
            Destination = destination;
            Priority = priority;
            AtWeekend = atWeekend;
            PickupDate = pickupDate;
            DeliveryDate = deliveryDate;
            Notes = string.Empty;
            LastUpdate = createdAt;
        }

        public static Delivery Create(AggregateId id, Guid orderId, DateTime dateTime, DeliveryStatus status,
            double volume, double weight, Address source, Address destination, Priority priority, 
            bool atWeekend, DateTime pickupDate, DateTime deliveryDate)
        {
            var delivery = new Delivery(id, orderId, dateTime, status, volume, weight, source, destination, 
                priority, atWeekend, pickupDate, deliveryDate);
            delivery.AddEvent(new DeliveryStateChanged(delivery));

            return delivery;
        }
        public void AssignCourier(DateTime dateTime, Guid courierId)
        {
            if(Status is not DeliveryStatus.Unassigned)
            {
                throw new CannotChangeDeliveryStateException(Id, Status, DeliveryStatus.Assigned);
            }
            if (CourierId != null)
            {
                throw new DeliveryHasAlreadyAssignedCourierException(Id, CourierId.Value);
            }

            CourierId = courierId;
            Status = DeliveryStatus.Assigned;
            LastUpdate = dateTime;
            AddEvent(new DeliveryStateChanged(this));
        }
        public void PickUp(DateTime dateTime)
        {
            if(Status is not DeliveryStatus.Assigned)
            {
                throw new CannotChangeDeliveryStateException(Id, Status, DeliveryStatus.InProgress);
            }

            Status = DeliveryStatus.InProgress;
            LastUpdate = dateTime;
            AddEvent(new DeliveryStateChanged(this));
        }

        public void Complete(DateTime dateTime)
        {
            if (Status is not DeliveryStatus.InProgress)
            {
                throw new CannotChangeDeliveryStateException(Id, Status, DeliveryStatus.Completed);
            }

            Status = DeliveryStatus.Completed;
            LastUpdate = dateTime;
            AddEvent(new DeliveryStateChanged(this));
        }

        public void Fail(DateTime dateTime, string reason)
        {
            if(Status is not DeliveryStatus.InProgress)
            {
                throw new CannotChangeDeliveryStateException(Id, Status, DeliveryStatus.CannotDeliver);
            }

            Status = DeliveryStatus.CannotDeliver;
            LastUpdate = dateTime;
            Notes = reason;
            AddEvent(new DeliveryStateChanged(this));
        }
    }
}