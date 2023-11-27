using SwiftParcel.Services.Orders.Core.Exceptions;

namespace SwiftParcel.Services.Orders.Core.Entities
{
    public class Order : AggregateRoot
    {
        private ISet<Parcel> _parcels = new HashSet<Parcel>();
        public Guid CustomerId { get; private set; }
        public Guid? CourierId { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ReceivedAt { get; private set; }
        public DateTime? DeliveredAt { get; private set; }
        public DateTime? CannotDeliverAt { get; private set; }
        public DateTime? DeliveryDate { get; private set; }
        public decimal TotalPrice { get; private set; }
        public string RejectionReason { get; private set; }
        public string CannotDeliverReason { get; private set; }
        public bool CanBeDeleted => Status == OrderStatus.New;
        public bool CanAssignCourier => Status == OrderStatus.New || Status == OrderStatus.Rejected;
        public bool HasParcels => Parcels.Any();

        public IEnumerable<Parcel> Parcels
        {
            get => _parcels;
            private set => _parcels = new HashSet<Parcel>(value);
        }

        public Order(AggregateId id, Guid customerId, OrderStatus status, DateTime createdAt,
            IEnumerable<Parcel> parcels = null, Guid? courierId = null, DateTime? deliveryDate = null,
            decimal totalPrice = 0)
        {
            Id = id;
            CustomerId = customerId;
            Status = status;
            CreatedAt = createdAt;
            Parcels = parcels ?? Enumerable.Empty<Parcel>();
            if (courierId.HasValue)
            {
                SetCourier(courierId.Value);
            }

            if (deliveryDate.HasValue)
            {
                SetDeliveryDate(deliveryDate.Value);
            }

            TotalPrice = totalPrice;
            RejectionReason = string.Empty;
            CannotDeliverReason = string.Empty;
        }

        public static Order Create(AggregateId id, Guid customerId, OrderStatus status, DateTime createdAt)
        {
            var order = new Order(id, customerId, status, createdAt);
            order.AddEvent(new OrderStateChanged(order));

            return order;
        }

        public void SetTotalPrice(decimal totalPrice)
        {
            if (Status != OrderStatus.New)
            {
                throw new CannotChangeOrderPriceException(Id);
            }

            if (totalPrice < 0)
            {
                throw new InvalidOrderPriceException(Id, totalPrice);
            }

            TotalPrice = totalPrice;
        }

        public void SetCourier(Guid courierId)
        {
            CourierId = courierId;
        }

        public void SetDeliveryDate(DateTime deliveryDate)
        {
            DeliveryDate = deliveryDate.Date;
        }

        public void AddParcel(Parcel parcel)
        {
            if (!_parcels.Add(parcel))
            {
                throw new ParcelAlreadyAddedToOrderException(Id, parcel.Id);
            }

            AddEvent(new ParcelAdded(this, parcel));
        }

        public void DeleteParcel(Guid parcelId)
        {
            var parcel = _parcels.SingleOrDefault(p => p.Id == parcelId);
            if (parcel is null)
            {
                throw new OrderParcelNotFoundException(parcelId, Id);
            }

            AddEvent(new ParcelDeleted(this, parcel));
        }

        public void Approve()
        {
            if (Status != OrderStatus.New && Status != OrderStatus.Rejected)
            {
                throw new CannotChangeOrderStateException(Id, Status, OrderStatus.Approved);
            }

            Status = OrderStatus.Approved;
            RejectionReason = string.Empty;
            AddEvent(new OrderStateChanged(this));
        }

        public void Reject(string reason)
        {
            if (Status == OrderStatus.Delivered || Status == OrderStatus.Rejected)
            {
                throw new CannotChangeOrderStateException(Id, Status, OrderStatus.Rejected);
            }

            Status = OrderStatus.Rejected;
            RejectionReason = reason ?? string.Empty;
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
            if (Status != OrderStatus.Approved && Status != OrderStatus.Received)
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

