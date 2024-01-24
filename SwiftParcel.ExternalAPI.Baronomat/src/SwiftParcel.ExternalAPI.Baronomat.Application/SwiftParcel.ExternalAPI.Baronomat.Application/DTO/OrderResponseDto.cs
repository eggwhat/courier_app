namespace SwiftParcel.ExternalAPI.Baronomat.Application.DTO
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public OrderAddressDto SenderAddress { get; set; }
        public OrderAddressDto RecipientAddress { get; set; }
        public ApiUserDto ApiUser { get; set; }
        public string OrderStatus { get; set; }
        public int PriceCents { get; set; }
        public int ShipmentWidthMm { get; set; }
        public int ShipmentHeightMm { get; set; }
        public int ShipmentLengthMm { get; set; }
        public int ShipmentWeightMg { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Priority { get; set; }
        public bool WeekendDelivery { get; set; }
    }   

    public class ApiUserDto
    {
        public int Id { get; set; }
        public object User { get; set; }
        public string HostName { get; set; }
    }
}