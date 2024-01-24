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
            SenderAddress = new OrderAddressDto(command.Parcel.Source, command.Name, command.Email);
            RecipientAddress = new OrderAddressDto(command.Parcel.Destination, command.Name, command.Email);
            PriceCents = (int)(command.Parcel.CalculatedPrice * 100);
            ShipmentWidthMm = (int)(command.Parcel.Width * 1000);
            ShipmentHeightMm = (int)(command.Parcel.Height * 1000);
            ShipmentLengthMm = (int)(command.Parcel.Depth * 1000);
            ShipmentWeightMg = (int)(command.Parcel.Weight * 1000);
            DeliveryDate = command.Parcel.DeliveryDate.ToString("yyyy-MM-dd");
            HighPriority = command.Parcel.Priority == "High";
            WeekendDelivery = command.Parcel.AtWeekend;
        }
    }
}