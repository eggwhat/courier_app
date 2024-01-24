using Convey.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.ExternalAPI.Baronomat.Application.Commands
{
    public class AddParcel : ICommand
    {
        public Guid ParcelId { get; protected set; }
        public Guid CustomerId { get; protected set; }
        public string Description { get; protected set; }
        public double Width { get; protected set; }
        public double Height { get; protected set; }
        public double Depth { get; protected set; }
        public double Weight { get; protected set; }
        public string SourceStreet { get; protected set; }
        public string SourceBuildingNumber { get; protected set; }
        public string SourceApartmentNumber { get; protected set; }
        public string SourceCity { get; protected set; }
        public string SourceZipCode { get; protected set; }
        public string SourceCountry { get; protected set; }
        public string DestinationStreet { get; protected set; }
        public string DestinationBuildingNumber { get; protected set; }
        public string DestinationApartmentNumber { get; protected set; }
        public string DestinationCity { get; protected set; }
        public string DestinationZipCode { get; protected set; }
        public string DestinationCountry { get; protected set; }
        public string Priority { get; protected set; }
        public bool AtWeekend { get; protected set; }
        public string PickupDate { get; protected set; }
        public string DeliveryDate { get; protected set; }
        public bool IsCompany { get; protected set; }
        public bool VipPackage { get; protected set; }

        public AddParcel(Guid parcelId, Guid customerId, string description,
            double width, double height, double depth, double weight,
            string sourceStreet, string sourceBuildingNumber,
            string sourceApartmentNumber, string sourceCity, string sourceZipCode,
            string sourceCountry, string destinationStreet, string destinationBuildingNumber,
            string destinationApartmentNumber, string destinationCity, string destinationZipCode,
            string destinationCountry, string priority, bool atWeekend, string pickupDate, 
            string deliveryDate, bool isCompany, bool vipPackage)
        {
            ParcelId = parcelId == Guid.Empty ? Guid.NewGuid() : parcelId;
            CustomerId = customerId;
            Description = description;
            Width = width;
            Height = height;
            Depth = depth;
            Weight = weight;

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

            Priority = priority;
            AtWeekend = atWeekend;
            PickupDate = pickupDate;
            DeliveryDate = deliveryDate;
            IsCompany = isCompany;
            VipPackage = vipPackage;
        }
    }
}
