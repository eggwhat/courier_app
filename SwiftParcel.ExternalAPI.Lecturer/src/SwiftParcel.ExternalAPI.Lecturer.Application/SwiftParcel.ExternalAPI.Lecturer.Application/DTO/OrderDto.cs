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
            Status = status;
            RequestValidTo = validTo;
            CourierCompany = company;
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
                Source = new AddressDto
                {
                    BuildingNumber = offer.Source.HouseNumber,
                    ApartmentNumber = offer.Source.ApartmentNumber,
                    Country = offer.Source.Country,
                    City = offer.Source.City,
                    Street = offer.Source.Street,
                    ZipCode = offer.Source.ZipCode
                },
                Destination = new AddressDto
                {
                    BuildingNumber = offer.Destination.HouseNumber,
                    ApartmentNumber = offer.Destination.ApartmentNumber,
                    Country = offer.Destination.Country,
                    City = offer.Destination.City,
                    Street = offer.Destination.Street,
                    ZipCode = offer.Destination.ZipCode
                },
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
            BuyerAddress = new AddressDto
            {
                BuildingNumber = offer.BuyerAddress.HouseNumber,
                ApartmentNumber = offer.BuyerAddress.ApartmentNumber,
                Country = offer.BuyerAddress.Country,
                City = offer.BuyerAddress.City,
                Street = offer.BuyerAddress.Street,
                ZipCode = offer.BuyerAddress.ZipCode
            };
            DecisionDate = offer.DecisionDate;
            PickedUpAt = DateTime.MaxValue;
            DeliveredAt = DateTime.MaxValue;
            CannotDeliverAt = DateTime.MaxValue;
            CancellationReason = string.Empty;
            CannotDeliverReason = string.Empty;
        }
    }
}