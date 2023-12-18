using SwiftParcel.Services.Orders.Core.Entities;
namespace SwiftParcel.Services.Orders.Application.DTO
{
    public class ParcelDto
    {
        public Guid Id { get; set; }
        public Guid? CustomerId { get; set; }  
        public string Description { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
        public double Weight { get; set; }
        public Address Source { get; set; }
        public Address Destination { get; set; }
        public Priority Priority { get; set; }
        public bool AtWeekend { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool IsCompany { get; set; }
        public bool VipPackage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ValidTo { get; set; }
        public decimal CalculatedPrice { get; set; }

        public ParcelDto()
        {
        }

        public ParcelDto(Parcel parcel)
        {
            Id = parcel.Id;
            CustomerId = parcel.CustomerId;
            Description = parcel.Description;
            Width = parcel.Width;
            Height = parcel.Height;
            Depth = parcel.Depth;
            Weight = parcel.Weight;
            Source = parcel.Source;
            Destination = parcel.Destination;
            Priority = parcel.Priority;
            AtWeekend = parcel.AtWeekend;
            PickupDate = parcel.PickupDate;
            DeliveryDate = parcel.DeliveryDate;
            IsCompany = parcel.IsCompany;
            VipPackage = parcel.VipPackage;
            CreatedAt = parcel.InquireDate;
            ValidTo = parcel.ValidTo;
            CalculatedPrice = parcel.CalculatedPrice;
        }
    }
}

