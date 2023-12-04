using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Application.DTO
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? CourierId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ReceivedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public DateTime? CannotDeliverAt { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<ParcelDto> Parcels { get; set; }

        public OrderDto()
        {
        }

        public OrderDto(Order order)
        {
            Id = order.Id;
            CustomerId = order.CustomerId;
            CourierId = order.CourierId;
            Status = order.Status.ToString().ToLowerInvariant();
            CreatedAt = order.CreatedAt;
            ReceivedAt = order.ReceivedAt;
            DeliveredAt = order.DeliveredAt;
            CannotDeliverAt = order.CannotDeliverAt;
            DeliveryDate = order.DeliveryDate;
            TotalPrice = order.TotalPrice;
            Parcels = order.Parcels.Select(p => new ParcelDto(p));
        }
    }
}