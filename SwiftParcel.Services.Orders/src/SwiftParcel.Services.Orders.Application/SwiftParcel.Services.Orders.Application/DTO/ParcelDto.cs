using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Application.DTO
{
    public class ParcelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Variant { get; set; }
        public string Size { get; set; }

        public ParcelDto()
        {
        }

        public ParcelDto(Parcel parcel)
        {
            Id = parcel.Id;
            Name = parcel.Name;
            Variant = parcel.Variant;
            Size = parcel.Size;
        }
    }
}

