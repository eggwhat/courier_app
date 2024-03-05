using SwiftParcel.ExternalAPI.Baronomat.Core.Entities;
using SwiftParcel.ExternalAPI.Baronomat.Core.Mappers;

namespace SwiftParcel.ExternalAPI.Baronomat.Application.DTO
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid? CustomerId { get; set; }
        public ParcelDto Parcel { get; set; }
        public string Status { get; set; }
        public string CourierCompany { get; set; }
        public DateTime OrderRequestDate { get; set; }
        public DateTime RequestValidTo { get; set; }
        public string BuyerName { get; set; }
        public string BuyerEmail { get; set; }
        public AddressDto BuyerAddress { get; set; }
        public DateTime? DecisionDate { get; set; }
        public DateTime? PickedUpAt { get; set; } 
        public DateTime? DeliveredAt { get; set; }
        public DateTime? CannotDeliverAt { get; set; }
        public string CancellationReason { get; set; }
        public string CannotDeliverReason { get; set; }

        public OrderDto()
        {

        }

        public OrderDto(OrderResponseDto offer, Guid customerId)
        {
            Id = Guid.Empty;
            CustomerId = customerId;
            Parcel = new ParcelDto
            {
                Id = Guid.Empty,
                CustomerId = customerId,
                Description = string.Empty,
                Width = (double)offer.ShipmentWidthMm / 1000,
                Height = (double)offer.ShipmentHeightMm / 1000,
                Depth = (double)offer.ShipmentLengthMm / 1000,
                Weight = (double) offer.ShipmentWeightMg / 1000,
                Source = new AddressDto(offer.SenderAddress),
                Destination = new AddressDto(offer.RecipientAddress),
                Priority = offer.Priority,
                AtWeekend = offer.WeekendDelivery,
                PickupDate = offer.DeliveryDate,
                DeliveryDate = offer.DeliveryDate,
                IsCompany = false,
                VipPackage = false,
                CreatedAt = DateTime.Now,
                ValidTo = DateTime.MaxValue,
                CalculatedPrice = (decimal)offer.PriceCents / 100,
                PriceBreakDown = new List<PriceBreakDownItemDto>()
                {
                    new PriceBreakDownItemDto()
                    {
                        Amount = (double)offer.PriceCents / 100,
                        Currency = "Pln",
                        Description = "Full price",
                    }
                }
            };
            Status = OrderStateToStatusMapper.Convert(offer.OrderStatus);
            CourierCompany = Company.Baronomat.ToString();
            OrderRequestDate = DateTime.Now.AddMinutes(-20);
            RequestValidTo = DateTime.MaxValue;
            BuyerName = $"{offer.SenderAddress.firstName} {offer.SenderAddress.surname}";
            BuyerEmail = offer.SenderAddress.email;
            BuyerAddress = new AddressDto(offer.SenderAddress);
            DecisionDate = offer.DeliveryDate;
            PickedUpAt = offer.DeliveryDate;
            DeliveredAt = offer.DeliveryDate;
            CannotDeliverAt = DateTime.MaxValue;
            CancellationReason = string.Empty;
            CannotDeliverReason = string.Empty;
        }
    }
}