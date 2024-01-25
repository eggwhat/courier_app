using SwiftParcel.ExternalAPI.Lecturer.Core.Entities;

namespace SwiftParcel.ExternalAPI.Lecturer.Application.DTO
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

        public OrderDto(Guid orderId, Guid customerId, string status, DateTime validTo,
            string company)
        {
            Id = orderId;
            CustomerId = customerId;
            Parcel = new ParcelDto
            {
                Id = Guid.Empty,
                CustomerId = customerId,
                Description = string.Empty,
                Width = 0,
                Height = 0,
                Depth = 0,
                Weight = 0,
                Source = new AddressDto("", "", "","","",""),
                Destination = new AddressDto("", "", "","","",""),
                Priority = "Low",
                AtWeekend = false,
                PickupDate = DateTime.MinValue,
                DeliveryDate = DateTime.MinValue,
                IsCompany = false,
                VipPackage = false,
                CreatedAt = DateTime.MinValue,
                ValidTo = DateTime.MinValue,
                CalculatedPrice = 0,            
            };
            Status = status.ToLower();
            OrderRequestDate = DateTime.Now;
            RequestValidTo = validTo;
            CourierCompany = company;
            BuyerName = string.Empty;
            BuyerEmail = string.Empty;
            BuyerAddress = new AddressDto("", "", "","","","");
            CancellationReason = string.Empty;
            CannotDeliverReason = string.Empty;
        }

        public OrderDto(OfferDto offer, Guid customerId, string status, string company)
        {
            Id = offer.OfferId;
            CustomerId = customerId;
            Parcel = new ParcelDto
            {
                Id = Guid.Empty,
                CustomerId = customerId,
                Description = string.Empty,
                Width = offer.Dimensions.Width,
                Height = offer.Dimensions.Height,
                Depth = offer.Dimensions.Length,
                Weight = offer.Weight,
                Source = new AddressDto(offer.Source),
                Destination = new AddressDto(offer.Destination),
                Priority = offer.Priority,
                AtWeekend = offer.DeliveryInWeekend,
                PickupDate = offer.PickupDate,
                DeliveryDate = offer.DeliveryDate,
                IsCompany = false,
                VipPackage = offer.VipPackage,
                CreatedAt = offer.InquireDate,
                ValidTo = DateTime.MinValue,
                CalculatedPrice = (decimal)offer.TotalPrice,
                PriceBreakDown = offer.PriceBreakDown
            };
            Status = status;
            CourierCompany = company;
            OrderRequestDate = offer.OfferRequestDate;
            RequestValidTo = offer.ValidTo;
            BuyerName = offer.BuyerName;
            BuyerEmail = string.Empty;
            BuyerAddress = new AddressDto(offer.BuyerAddress);
            DecisionDate = offer.DecisionDate;
            PickedUpAt = DateTime.MaxValue;
            DeliveredAt = DateTime.MaxValue;
            CannotDeliverAt = DateTime.MaxValue;
            CancellationReason = string.Empty;
            CannotDeliverReason = string.Empty;
        }
    }
}