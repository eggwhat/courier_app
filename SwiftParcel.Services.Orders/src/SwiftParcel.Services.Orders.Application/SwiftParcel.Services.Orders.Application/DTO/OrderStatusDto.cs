namespace SwiftParcel.Services.Orders.Application.DTO
{
    public class OrderStatusDto
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}