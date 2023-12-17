namespace SwiftParcel.Services.Parcels.Application.DTO
{
    public class ValidationStatusDto
    {
        public Guid ParcelId { get; set; } 
        public decimal TotalPrice { get; set; }
        public DateTime ExpiringAt { get; set; }
    }
}