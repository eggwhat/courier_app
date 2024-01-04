namespace SwiftParcel.ExternalAPI.Lecturer.Application.DTO
{
    public class InquiryResponseDto
    {
        public Guid InquiryId { get; set; }
        public double TotalPrice { get; set; }
        public string Currency { get; set; }
        public DateTime ExpiringAt { get; set; }
        public PriceBreakDownDto PriceBreakDown { get; set; }
    }
}