using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Application.DTO;
using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class CreateOrderBaronomat: ICommand
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public Guid ParcelId { get; }
        public string SourceStreet { get; }
        public string SourceBuildingNumber { get; }
        public string SourceApartmentNumber { get; }
        public string SourceCity { get; }
        public string SourceZipCode { get; }
        public string SourceCountry { get; }
        public string DestinationStreet { get; }
        public string DestinationBuildingNumber { get; }
        public string DestinationApartmentNumber { get; }
        public string DestinationCity { get; }
        public string DestinationZipCode { get; }
        public string DestinationCountry { get; }
        public double Price { get; }
        public double Width { get; }
        public double Height { get; }
        public double Depth { get; }
        public double Weight { get; }
        public DateTime DeliveryDate { get; }
        public string Priority { get; }
        public bool AtWeekend { get; }
        public string Name { get; }
        public string Email { get; }

        public CreateOrderBaronomat(CreateOrder command, ParcelDto parcel)
        {
            OrderId = command.OrderId;
            CustomerId = command.CustomerId;
            ParcelId = parcel.Id;
            SourceStreet = parcel.Source.Street;
            SourceBuildingNumber = parcel.Source.BuildingNumber;
            SourceApartmentNumber = parcel.Source.ApartmentNumber;
            SourceCity = parcel.Source.City;
            SourceZipCode = parcel.Source.ZipCode;
            SourceCountry = parcel.Source.Country;
            DestinationStreet = parcel.Destination.Street;
            DestinationBuildingNumber = parcel.Destination.BuildingNumber;
            DestinationApartmentNumber = parcel.Destination.ApartmentNumber;
            DestinationCity = parcel.Destination.City;
            DestinationZipCode = parcel.Destination.ZipCode;
            DestinationCountry = parcel.Destination.Country;
            Price = (double)parcel.CalculatedPrice;
            Width = parcel.Width;
            Height = parcel.Height;
            Depth = parcel.Depth;
            Weight = parcel.Weight;
            DeliveryDate = parcel.DeliveryDate;
            Priority = parcel.Priority.ToString();
            AtWeekend = parcel.AtWeekend;
            Name = command.Name;
            Email = command.Email;
        }
    }
}
