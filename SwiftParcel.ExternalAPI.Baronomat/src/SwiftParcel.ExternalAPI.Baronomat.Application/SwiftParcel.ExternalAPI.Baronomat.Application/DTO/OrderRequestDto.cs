using SwiftParcel.ExternalAPI.Baronomat.Application.Commands;

namespace SwiftParcel.ExternalAPI.Baronomat.Application.DTO
{
    public class OrderRequestDto
    {
        public OrderAddressDto SenderAddress { get; set; }
        public OrderAddressDto RecipientAddress { get; set; }
        public int PriceCents { get; set; }
        public int ShipmentWidthMm { get; set; }
        public int ShipmentHeightMm { get; set; }
        public int ShipmentLengthMm { get; set; }
        public int ShipmentWeightMg { get; set; }
        public string DeliveryDate { get; set; }
        public bool HighPriority { get; set; }
        public bool WeekendDelivery { get; set; }

        public OrderRequestDto(CreateOrder command)
        {
            var source = new AddressDto(command.SourceStreet, command.SourceBuildingNumber, command.SourceApartmentNumber, command.SourceCity, command.SourceZipCode, command.SourceCountry);
            var destination = new AddressDto(command.DestinationStreet, command.DestinationBuildingNumber, command.DestinationApartmentNumber, command.DestinationCity, command.DestinationZipCode, command.DestinationCountry);
            SenderAddress = new OrderAddressDto(source, command.Name, command.Email);
            RecipientAddress = new OrderAddressDto(destination, command.Name, command.Email);
            PriceCents = (int)(command.Price * 100);
            ShipmentWidthMm = (int)(command.Width * 1000);
            ShipmentHeightMm = (int)(command.Height * 1000);
            ShipmentLengthMm = (int)(command.Depth * 1000);
            ShipmentWeightMg = (int)(command.Weight * 1000);
            DeliveryDate = command.DeliveryDate.ToString("yyyy-MM-dd");
            HighPriority = command.Priority == "High";
            WeekendDelivery = command.AtWeekend;
        }
    }
}