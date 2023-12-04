using Convey.CQRS.Commands;
using SwiftParcel.Services.Parcels.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Application.Commands
{
    public class AddParcel : ICommand
    {
        public Guid ParcelId { get; protected set; }
        public Guid CustomerId { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public double Width { get; protected set; }
        public double Height { get; protected set; }
        public double Depth { get; protected set; }
        public double Weight { get; protected set; }
        public decimal Price { get; protected set; }
        public string SourceStreet { get; protected set; }
        public string SourceBuildingNumber { get; protected set; }
        public string SourceApartmentNumber { get; protected set; }
        public string SourceCity { get; protected set; }
        public string SourceZipCode { get; protected set; }
        public string DestinationStreet { get; protected set; }
        public string DestinationBuildingNumber { get; protected set; }
        public string DestinationApartmentNumber { get; protected set; }
        public string DestinationCity { get; protected set; }
        public string DestinationZipCode { get; protected set; }
        public string Variant { get; protected set; }
        public string Priority { get; protected set; }
        public bool AtWeekend { get; protected set; }
        public bool IsFragile { get; protected set; }

        public AddParcel(Guid parcelId, Guid customerId, string name, string description,
            double width, double height, double depth, double weight, decimal price,
            string sourceStreet, string sourceBuildingNumber,
            string sourceApartmentNumber, string sourceCity, string sourceZipCode,
            string destinationStreet, string destinationBuildingNumber,
            string destinationApartmentNumber, string destinationCity, string destinationZipCode,
            string variant, string priority, bool atWeekend, bool isFragile)
        {
            ParcelId = parcelId == Guid.Empty ? Guid.NewGuid() : parcelId;
            CustomerId = customerId;
            Name = name;
            Description = description;
            Width = width;
            Height = height;
            Depth = depth;
            Weight = weight;
            Price = price;

            SourceStreet = sourceStreet;
            SourceBuildingNumber = sourceBuildingNumber;
            SourceApartmentNumber = sourceApartmentNumber;
            SourceCity = sourceCity;
            SourceZipCode = sourceZipCode;

            DestinationStreet = destinationStreet;
            DestinationBuildingNumber = destinationBuildingNumber;
            DestinationApartmentNumber = destinationApartmentNumber;
            DestinationCity = destinationCity;
            DestinationZipCode = destinationZipCode;

            Variant = variant;
            Priority = priority;
            AtWeekend = atWeekend;
            IsFragile = isFragile;
        }
    }
}
