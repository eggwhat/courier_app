using SwiftParcel.ExternalAPI.Lecturer.Core.Entities;

namespace SwiftParcel.ExternalAPI.Lecturer.Application.DTO
{
    public class AddressDto
    {
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public AddressDto(string street, string buildingNumber, string apartmentNumber, string city, string zipCode, string country)
        {
            Street = street;
            BuildingNumber = buildingNumber;
            ApartmentNumber = apartmentNumber;
            City = city;
            ZipCode = zipCode;
            Country = country;
        }

        public AddressDto(InquiryAddressDto address)
        {
            Street = address.Street;
            BuildingNumber = address.HouseNumber;
            ApartmentNumber = address.ApartmentNumber;
            City = address.City;
            ZipCode = address.ZipCode;
            Country = address.Country;
        }
    }
}