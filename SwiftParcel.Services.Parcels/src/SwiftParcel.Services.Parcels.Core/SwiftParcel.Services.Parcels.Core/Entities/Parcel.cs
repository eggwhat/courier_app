using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SwiftParcel.Services.Parcels.Core.Exceptions;
using SwiftParcel.Services.Parcels.Core.Exceptions.SwiftParcel.Services.Parcels.Core.Exceptions;

namespace SwiftParcel.Services.Parcels.Core.Entities
{
    public class Parcel
    {
        public Guid Id { get; protected set; }
        public Guid? CustomerId { get; protected set; }  
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
        public DateTime CreatedAt { get; protected set; }
        public DateTime? ValidTo { get; protected set; }
        public decimal? CalculatedPrice { get; protected set; }

        public Parcel(AggregateId id, string description, double width,
            double height, double depth, double weight, DateTime pickupDate, DateTime deliveryDate,
            DateTime createdAt, Guid? customerId = null, decimal? calculatedPrice = null, DateTime? validTo = null)
            : this(id, description, width, height, depth, weight, new Address(),
                  new Address(), Priority.Low, false, pickupDate, deliveryDate, false, false, createdAt, customerId, calculatedPrice, validTo)
        {

        }

        public Parcel(AggregateId id, string description, double width, double height,
            double depth, double weight, Address source, Address destination,
            Priority priority, bool atWeekend, DateTime pickupDate, DateTime deliveryDate, bool isCompany, bool vipPackage,
             DateTime createdAt, Guid? customerId = null, decimal? calculatedPrice = null, DateTime? validTo = null)
        {
            Id = id;
            CustomerId = customerId;

            CheckDescription(description);
            Description = description;
            
            CheckDimensions(width, height, depth);
            Width = width;
            Height = height;
            Depth = depth;
            
            CheckWeight(weight);
            Weight = weight;
                        
            Source = source;
            Destination = destination;
            Priority = priority;
            AtWeekend = atWeekend;
            PickupDate = pickupDate;
            DeliveryDate = deliveryDate;
            IsCompany = isCompany;
            VipPackage = vipPackage;
            CreatedAt = createdAt;
            ValidTo = validTo;
            CalculatedPrice = calculatedPrice;
        }

        public void CheckDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new InvalidParcelDescriptionException(description);
            }
        }

        public void CheckDimensions(double width, double height, double depth)
        {
            if (width <= 0)
            {
                throw new InvalidParcelDimensionException("width", width);
            }

            if (height <= 0)
            {
                throw new InvalidParcelDimensionException("height", height);
            }

            if (depth <= 0)
            {
                throw new InvalidParcelDimensionException("depth", depth);
            }
        }

        public void CheckWeight(double weight)
        {
            if (weight <= 0)
            {
                throw new InvalidParcelWeightException(weight);
            }
        }

        public void CheckPrice(decimal price)
        {
            if (price <= 0)
            {
                throw new InvalidParcelPriceException(price);
            }
        }
        public void SetCalculatedPrice(decimal price)
        {
            CheckPrice(price);
            CalculatedPrice = price;
        }

        public void SetSourceAddress(string street, string buildingNumber, string apartmentNumber, string city, string zipCode, string country)
        {
            SetAddress(Source, street, buildingNumber, apartmentNumber, city, zipCode, country);
        }

        public void SetDestinationAddress(string street, string buildingNumber, string apartmentNumber, string city, string zipCode, string country)
        {
            SetAddress(Destination, street, buildingNumber, apartmentNumber, city, zipCode, country);
        }

        private void SetAddress(Address address, string street, string buildingNumber, string apartmentNumber,
            string city, string zipCode, string country)
        {
            CheckAddressElement("street", street);
            address.Street = street;

            CheckAddressElement("building number", buildingNumber);
            address.BuildingNumber = buildingNumber;

            CheckAddressApartmentNumber("apartment number", ref apartmentNumber);
            address.ApartmentNumber = apartmentNumber;

            CheckAddressElement("city", city);
            address.City = city;

            CheckAddressZipCode("zip code", zipCode);
            address.ZipCode = zipCode;

            CheckAddressElement("country", country);
            address.Country = country;
        }

        public void CheckAddressElement(string element, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidAddressElementException(element, value);
            }
        }

        public void CheckAddressApartmentNumber(string element, ref string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                value = string.Empty;
            }
        }

        public void CheckAddressZipCode(string element, string value)
        {
            string pattern = @"\d{2}[-]\d{3}";
            if (!Regex.IsMatch(value, pattern))
            {
                throw new InvalidAddressElementException(element, value);
            }
        }
        public void SetPriority(Priority priority) => Priority = priority;

        public void SetAtWeekend(bool atWeekend) => AtWeekend = atWeekend;
    }
}
