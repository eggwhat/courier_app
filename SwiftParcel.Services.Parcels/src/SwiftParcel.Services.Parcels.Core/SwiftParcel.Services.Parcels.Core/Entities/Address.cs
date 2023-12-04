using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Core.Entities
{
    public class Address
    {
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        public Address()
        {
            Street = string.Empty;
            BuildingNumber = string.Empty;
            ApartmentNumber = string.Empty;
            City = string.Empty;
            ZipCode = string.Empty;
        }

        public Address(string street, string buildingNumber, string apartmentNumber, string city, string zipCode)
        {
            Street = street;
            BuildingNumber = buildingNumber;
            ApartmentNumber = apartmentNumber;
            City = city;
            ZipCode = zipCode;
        }
    }
}
