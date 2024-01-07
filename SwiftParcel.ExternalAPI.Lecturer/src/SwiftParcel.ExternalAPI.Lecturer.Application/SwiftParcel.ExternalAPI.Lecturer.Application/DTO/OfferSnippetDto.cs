namespace SwiftParcel.ExternalAPI.Lecturer.Application.DTO
{
    public class OfferSnippetDto
    {
        public Guid OfferRequestId { get; set; }
        public Guid? OfferId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime ValidTo { get; set; }
        public string Status { get; set; }
    }
}