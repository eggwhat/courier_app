using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SwiftParcel.Services.Parcels.Core.Exceptions;

namespace SwiftParcel.Services.Parcels.Core.Entities
{
    public class Parcel
    {
        public Guid Id { get; protected set; }
        public Guid OrderId { get; protected set; }
        public Guid CustomerId { get; protected set; }
        public string Description { get; protected set; }
        public double Width { get; protected set; }
        public double Height { get; protected set; }
        public double Depth { get; protected set; }
        public double Weight { get; protected set; }
        public decimal Price { get; protected set; }
        public Address Source { get; protected set; }
        public Address Destination { get; protected set; }
        public Variant Variant { get; protected set; }
        public Priority Priority { get; protected set; }
        public bool AtWeekend { get; protected set; }
        public bool IsFragile { get; protected set; }

        public Parcel(AggregateId id, Guid orderId, Guid customerId, string description,
            double width, double height, double depth, double weight, decimal price)
            : this(id, orderId, customerId, description, width, height, depth, weight, price,
                  new Address(), new Address(), Variant.Standard, Priority.Low, false, false)
        {

        }

        public Parcel(AggregateId id, Guid orderId, Guid customerId, string description, double width,
            double height, double depth, double weight, decimal price, Address source, Address destination,
            Variant variant, Priority priority, bool atWeekend, bool isFragile)
        {
            Id = id;
            OrderId = orderId;
            CustomerId = customerId;
            CheckDescription(description);
            Description = description;
            CheckDimensions(width, height, depth);
            Width = width;
            Height = height;
            Depth = depth;
            CheckWeight(weight);
            Weight = weight;
            CheckPrice(price);
            Price = price;
            Source = source;
            Destination = destination;
            Variant = variant;
            Priority = priority;
            AtWeekend = atWeekend;
            IsFragile = isFragile;
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

        public void SetSourceAddress(string street, string buildingNumber, string apartmentNumber, string city, string zipCode)
        {
            SetAddress(Source, street, buildingNumber, apartmentNumber, city, zipCode);
        }

        public void SetDestinationAddress(string street, string buildingNumber, string apartmentNumber, string city, string zipCode)
        {
            SetAddress(Destination, street, buildingNumber, apartmentNumber, city, zipCode);
        }

        private void SetAddress(Address address, string street, string buildingNumber, string apartmentNumber,
            string city, string zipCode)
        {
            CheckAddressElement("street", street);
            address.Street = street;

            CheckAddressElement("building number", buildingNumber);
            address.BuildingNumber = buildingNumber;

            CheckAddressElement("apartment number", apartmentNumber);
            address.ApartmentNumber = apartmentNumber;

            CheckAddressElement("city", city);
            address.City = city;

            CheckAddressElement("zip code", zipCode);
            address.ZipCode = zipCode;
        }

        public void CheckAddressElement(string element, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidAddressElementException(element, value);
            }
        }

        public void ChangeVariants(Variant variant) => Variant = variant;

        public void ChangePriority(Priority priority) => Priority = priority;
    }
}
