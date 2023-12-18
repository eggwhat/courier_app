using SwiftParcel.Services.Orders.Core.Exceptions;
using SwiftParcel.Services.Orders.Core.Events;

namespace SwiftParcel.Services.Orders.Core.Entities
{
    public class Order : AggregateRoot
    {
        public Guid? CustomerId { get; private set; }
        public Parcel Parcel { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime OrderRequestDate { get; private set; }
        public string BuyerName { get; private set; }
        public string BuyerEmail { get; private set; }
        public Address BuyerAddress { get; private set; }
        public DateTime? DecisionDate { get; private set; }
        public DateTime? ReceivedAt { get; private set; } 
        public DateTime? DeliveredAt { get; private set; }
        public DateTime? CannotDeliverAt { get; private set; }
        public string CancellationReason { get; private set; }
        public string CannotDeliverReason { get; private set; }
        public bool CanRequestDelivery => Status == OrderStatus.Approved;
        public bool HasParcel => Parcel != null;


        public Order(AggregateId id, Guid? customerId, OrderStatus status, DateTime createdAt,
            string buyerName, string buyerEmail, Address buyerAddress, Parcel parcel = null)
        {
            Id = id;
            CustomerId = customerId;
            Status = status;
            OrderRequestDate = createdAt;
            BuyerName = buyerName;
            BuyerEmail = buyerEmail;
            BuyerAddress = buyerAddress;
            Parcel = parcel;

            DecisionDate = null;
            ReceivedAt = null;
            DeliveredAt = null;
            CannotDeliverAt = null;
            CancellationReason = string.Empty;
            CannotDeliverReason = string.Empty;
        }

        public static Order Create(AggregateId id, Guid customerId, OrderStatus status, DateTime createdAt,
            string buyerName, string buyerEmail, Address buyerAddress)
        {
            var order = new Order(id, customerId == Guid.Empty ? null : customerId,
                        status, createdAt, buyerName, buyerEmail, buyerAddress);
            order.AddEvent(new OrderStateChanged(order));

            return order;
        }

        public void AddParcel(Parcel parcel)
        {
            if (HasParcel)
            {
                throw new ParcelAlreadyAddedToOrderException(Id, parcel.Id);
            }
            Parcel = parcel;
            AddEvent(new ParcelAdded(this, parcel));
        }

        public void Approve(DateTime decidedAt)
        {
            if (Status != OrderStatus.New && Status != OrderStatus.Cancelled)
            {
                throw new CannotChangeOrderStateException(Id, Status, OrderStatus.Approved);
            }

            DecisionDate = decidedAt;
            Status = OrderStatus.Approved;
            CancellationReason = string.Empty;
            AddEvent(new OrderStateChanged(this));
        }

        public void Cancel(DateTime decidedAt, string reason)
        {
            if (Status == OrderStatus.Delivered || Status == OrderStatus.Cancelled)
            {
                throw new CannotChangeOrderStateException(Id, Status, OrderStatus.Cancelled);
            }

            DecisionDate = decidedAt;
            Status = OrderStatus.Cancelled;
            CancellationReason = reason ?? string.Empty;
            AddEvent(new OrderStateChanged(this));
        }

        public void Deliver(DateTime deliveredAt)
        {
            if (Status != OrderStatus.Received)
            {
                throw new CannotChangeOrderStateException(Id, Status, OrderStatus.Delivered);
            }

            DeliveredAt = deliveredAt;
            Status = OrderStatus.Delivered;
            AddEvent(new OrderStateChanged(this));
        }

        public void SetReceived(DateTime receivedAt)
        {
            if (Status != OrderStatus.Approved)
            {
                throw new CannotChangeOrderStateException(Id, Status, OrderStatus.Received);
            }

            ReceivedAt = receivedAt;
            Status = OrderStatus.Received;
            AddEvent(new OrderStateChanged(this));
        }
        
        public void SetCannotDeliver(string reason, DateTime cannotDeliverAt)
        {
            if (Status != OrderStatus.Received)
            {
                throw new CannotChangeOrderStateException(Id, Status, OrderStatus.CannotDeliver);
            }

            CannotDeliverAt = cannotDeliverAt;
            Status = OrderStatus.CannotDeliver;
            CannotDeliverReason = reason ?? string.Empty;
            AddEvent(new OrderStateChanged(this));
        }
    }
}

