using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Application.DTO
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

        public OrderDto(Order order)
        {
            Id = order.Id;
            CustomerId = order.CustomerId;
            Parcel = order.Parcel == null ? null : new ParcelDto(order.Parcel);
            Status = order.Status.ToString().ToLowerInvariant();
            CourierCompany = order.CourierCompany.ToString();
            OrderRequestDate = order.OrderRequestDate;
            RequestValidTo = order.RequestValidTo;
            BuyerName = order.BuyerName;
            BuyerEmail = order.BuyerEmail;
            BuyerAddress = new AddressDto(order.BuyerAddress.Street, order.BuyerAddress.BuildingNumber,
                order.BuyerAddress.ApartmentNumber, order.BuyerAddress.City, order.BuyerAddress.ZipCode,
                order.BuyerAddress.Country);
            DecisionDate = order.DecisionDate;
            PickedUpAt = order.PickedUpAt;
            DeliveredAt = order.DeliveredAt;
            CannotDeliverAt = order.CannotDeliverAt;
            CancellationReason = order.CancellationReason;
            CannotDeliverReason = order.CannotDeliverReason;
        }
    }
}