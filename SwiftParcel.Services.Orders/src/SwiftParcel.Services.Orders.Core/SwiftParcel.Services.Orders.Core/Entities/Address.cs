namespace SwiftParcel.Services.Orders.Core.Entities
{
    public class Address
    {
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set;}

        public Address()
        {
        }

        public Address(string street, string buildingNumber, string apartmentNumber, string city, string zipCode, string country)
        {
            Street = street;
            BuildingNumber = buildingNumber;
            ApartmentNumber = apartmentNumber;
            City = city;
            ZipCode = zipCode;
            Country = country;
        }
    }
}