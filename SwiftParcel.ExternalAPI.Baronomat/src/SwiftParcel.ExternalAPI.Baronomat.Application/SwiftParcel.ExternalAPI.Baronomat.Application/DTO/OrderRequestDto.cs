using SwiftParcel.ExternalAPI.Baronomat.Application.Commands;

namespace SwiftParcel.ExternalAPI.Baronomat.Application.DTO
{
    public class OrderRequestDto
    {
        public OrderAddressDto senderAddress { get; set; }
        public OrderAddressDto recipientAddress { get; set; }
        public int priceCents { get; set; }
        public int shipmentWidthMm { get; set; }
        public int shipmentHeightMm { get; set; }
        public int shipmentLengthMm { get; set; }
        public int shipmentWeightMg { get; set; }
        public string deliveryDate { get; set; }
        public bool highPriority { get; set; }
        public bool weekendDelivery { get; set; }

        public OrderRequestDto(CreateOrder command, double price)
        {
            var source = new AddressDto(command.SourceStreet, command.SourceBuildingNumber, command.SourceApartmentNumber, command.SourceCity, command.SourceZipCode, command.SourceCountry);
            var destination = new AddressDto(command.DestinationStreet, command.DestinationBuildingNumber, command.DestinationApartmentNumber, command.DestinationCity, command.DestinationZipCode, command.DestinationCountry);
            senderAddress = new OrderAddressDto(source, command.Name, command.Email);
            recipientAddress = new OrderAddressDto(destination, command.Name, command.Email);
            priceCents = (int)(price * 100);
            shipmentWidthMm = (int)(command.Width * 1000);
            shipmentHeightMm = (int)(command.Height * 1000);
            shipmentLengthMm = (int)(command.Depth * 1000);
            shipmentWeightMg = (int)(command.Weight * 1000);
            deliveryDate = command.DeliveryDate.ToString("yyyy-MM-dd");
            highPriority = command.Priority == "High";
            weekendDelivery = command.AtWeekend;
        }
    }
}