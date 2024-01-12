using SwiftParcel.Services.Orders.Core.Exceptions;

namespace SwiftParcel.Services.Orders.Core.Entities
{
    public class Parcel : IEquatable<Parcel>
    {
        public Guid Id { get; protected set; }
        public string Description { get; protected set; }
        public double Width { get; protected set; }
        public double Height { get; protected set; }
        public double Depth { get; protected set; }
        public double Weight { get; protected set; }
        public Address Source { get; protected set; }
        public Address Destination { get; protected set; }
        public Priority Priority { get; protected set; }
        public bool AtWeekend { get; protected set; }
        public DateTime PickupDate { get; protected set; }
        public DateTime DeliveryDate { get; protected set; }
        public bool IsCompany { get; protected set; }
        public bool VipPackage { get; protected set; }
        public DateTime InquireDate { get; protected set; }
        public DateTime ValidTo { get; protected set; }
        public decimal CalculatedPrice { get; protected set; }
        public List<PriceBreakDownItem> PriceBreakDown { get; protected set; }

        public Parcel(Guid id, string description, double width, double height, 
            double depth, double weight, Address source, Address destination, Priority priority, bool atWeekend,
            DateTime pickupDate, DateTime deliveryDate, bool isCompany, bool vipPackage, DateTime createdAt, 
            DateTime validTo, decimal calculatedPrice, List<PriceBreakDownItem> priceBreakDown)
        {
            Id = id;
            Description = description;
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
            IsCompany = isCompany;
            VipPackage = vipPackage;
            InquireDate = createdAt;
            ValidTo = validTo;
            CalculatedPrice = calculatedPrice;
            PriceBreakDown = priceBreakDown;
        }
        public void ValidateRequest(DateTime requestDate)
        {
            if (requestDate > ValidTo)
            {
                throw new ParcelRequestExpiredException(Id, ValidTo, requestDate);
            }
        }

        public bool Equals(Parcel other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Parcel) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}

