using System.Text.Json.Serialization;

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

        [JsonConstructor]
        public OrderResponseDto(int id, OrderAddressDto senderAddress, OrderAddressDto recipientAddress, ApiUserDto apiUser, string orderStatus, int priceCents, int shipmentWidthMm, int shipmentHeightMm, int shipmentLengthMm, int shipmentWeightMg, DateTime deliveryDate, string priority, bool weekendDelivery)
        {
            Id = id;
            SenderAddress = senderAddress;
            RecipientAddress = recipientAddress;
            ApiUser = apiUser != null ? new ApiUserDto(apiUser.Id, apiUser.User, apiUser.HostName) : new ApiUserDto();
            OrderStatus = orderStatus;
            PriceCents = priceCents;
            ShipmentWidthMm = shipmentWidthMm;
            ShipmentHeightMm = shipmentHeightMm;
            ShipmentLengthMm = shipmentLengthMm;
            ShipmentWeightMg = shipmentWeightMg;
            DeliveryDate = deliveryDate;
            Priority = priority;
            WeekendDelivery = weekendDelivery;
        }
    }   

    public class ApiUserDto
    {
        public int Id { get; set; }
        public object User { get; set; }
        public string HostName { get; set; }
        [JsonConstructor]
        public ApiUserDto(int id, object user, string hostName)
        {
            Id = id;
            User = user;
            HostName = hostName;
        }
        public ApiUserDto()
        {
        }
    }
}