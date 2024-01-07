namespace SwiftParcel.ExternalAPI.Lecturer.Application.DTO
{
    public class InquiryDto
    {
        public DimensionDto Dimensions { get; set; }
        public string Currency { get; set; }
        public double Weight { get; set; }
        public string WeightUnit { get; set; }
        public InquiryAddressDto Source { get; set; }
        public InquiryAddressDto Destination { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDay { get; set; }
        public bool DeliveryInWeekend { get; set; }
        public string Priority { get; set; }
        public bool VipPackage { get; set; }
        public bool IsComapny { get; set; }

        public InquiryDto(double width, double height, double length, string dimensionUnit, string currency, 
            double weight, string weightUnit, string sourceHouseNumber, string sourceApartmentNumber, string sourceStreet,
            string sourceCity, string sourceZipCode, string sourceCountry, string destinationHouseNumber, 
            string destinationApartmentNumber, string destinationStreet, string destinationCity, string destinationZipCode, 
            string destinationCountry, DateTime pickupDate, DateTime deliveryDay, bool deliveryInWeekend, string priority, 
            bool vipPackage, bool isComapny)
        {
            Dimensions = new DimensionDto
            {
                Width = width,
                Height = height,
                Length = length,
                DimensionUnit = dimensionUnit
            };
            Currency = currency;
            Weight = weight;
            WeightUnit = weightUnit;
            Source = new InquiryAddressDto
            {
                HouseNumber = sourceHouseNumber,
                ApartmentNumber = sourceApartmentNumber,
                Street = sourceStreet,
                City = sourceCity,
                ZipCode = sourceZipCode,
                Country = sourceCountry
            };
            Destination = new InquiryAddressDto
            {
                HouseNumber = destinationHouseNumber,
                ApartmentNumber = destinationApartmentNumber,
                Street = destinationStreet,
                City = destinationCity,
                ZipCode = destinationZipCode,
                Country = destinationCountry
            };
            PickupDate = pickupDate;
            DeliveryDay = deliveryDay;
            DeliveryInWeekend = deliveryInWeekend;
            Priority = priority;
            VipPackage = vipPackage;
            IsComapny = isComapny;
        }

    }

    public class DimensionDto
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
        public string DimensionUnit { get; set; }
    }

    public class InquiryAddressDto
    {
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}