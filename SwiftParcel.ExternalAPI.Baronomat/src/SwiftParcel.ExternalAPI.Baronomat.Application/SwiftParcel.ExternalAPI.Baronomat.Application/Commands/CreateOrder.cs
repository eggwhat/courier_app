using Convey.CQRS.Commands;
using SwiftParcel.ExternalAPI.Baronomat.Application.DTO;
using SwiftParcel.ExternalAPI.Baronomat.Core.Entities;

namespace SwiftParcel.ExternalAPI.Baronomat.Application.Commands
{
    public class CreateOrder: ICommand
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

        public CreateOrder(Guid orderId, Guid customerId, Guid parcelId, string sourceStreet, string sourceBuildingNumber, string sourceApartmentNumber, string sourceCity, string sourceZipCode, string sourceCountry, string destinationStreet, string destinationBuildingNumber, string destinationApartmentNumber, string destinationCity, string destinationZipCode, string destinationCountry, double price, double width, double height, double depth, double weight, DateTime deliveryDate, string priority, bool atWeekend, string name, string email)
        {
            OrderId = orderId;
            CustomerId = customerId;
            ParcelId = parcelId;
            SourceStreet = sourceStreet;
            SourceBuildingNumber = sourceBuildingNumber;
            SourceApartmentNumber = sourceApartmentNumber;
            SourceCity = sourceCity;
            SourceZipCode = sourceZipCode;
            SourceCountry = sourceCountry;
            DestinationStreet = destinationStreet;
            DestinationBuildingNumber = destinationBuildingNumber;
            DestinationApartmentNumber = destinationApartmentNumber;
            DestinationCity = destinationCity;
            DestinationZipCode = destinationZipCode;
            DestinationCountry = destinationCountry;
            DestinationCity = destinationCity;
            DestinationZipCode = destinationZipCode;
            DestinationCountry = destinationCountry;
            Price = price;
            Width = width;
            Height = height;
            Depth = depth;
            Weight = weight;
            DeliveryDate = deliveryDate;
            Priority = priority;
            AtWeekend = atWeekend;
            Name = name;
            Email = email;
        }
    }
}