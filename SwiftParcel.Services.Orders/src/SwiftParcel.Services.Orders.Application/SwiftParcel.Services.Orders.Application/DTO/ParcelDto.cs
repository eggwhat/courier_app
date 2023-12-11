using SwiftParcel.Services.Orders.Core.Entities;
namespace SwiftParcel.Services.Orders.Application.DTO
{
    public class ParcelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Variant { get; set; }
        public string Description { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
        public double Weight { get; set; }
        public decimal Price { get; set; }
        public Address Source { get; set; }
        public Address Destination { get; set; }

        public ParcelDto()
        {
        }

        public ParcelDto(Parcel parcel)
        {
            Id = parcel.Id;
            Name = parcel.Name;
            Variant = parcel.Variant;
            Description = parcel.Description;
            Width = parcel.Width;
            Height = parcel.Height;
            Depth = parcel.Depth;
            Weight = parcel.Weight;
            Price = parcel.Price;
            Source = parcel.Source;
            Destination = parcel.Destination;
        }
    }
}

