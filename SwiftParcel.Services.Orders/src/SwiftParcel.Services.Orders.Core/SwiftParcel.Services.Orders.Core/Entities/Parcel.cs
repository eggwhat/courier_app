namespace SwiftParcel.Services.Orders.Core.Entities
{
    public class Parcel : IEquatable<Parcel>
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Variant { get; }
        public string Description { get; protected set; }
        public double Width { get; protected set; }
        public double Height { get; protected set; }
        public double Depth { get; protected set; }
        public double Weight { get; protected set; }
        public decimal Price { get; protected set; }
        public Address Source { get; protected set; }
        public Address Destination { get; protected set; }

        public Parcel(Guid id, string name, string variant, string description, double width, double height,
            double depth, double weight, decimal price, Address source, Address destination)
        {
            Id = id;
            Name = name;
            Variant = variant;
            Description = description;
            Width = width;
            Height = height;
            Depth = depth;
            Weight = weight;
            Price = price;
            Source = source;
            Destination = destination;
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

