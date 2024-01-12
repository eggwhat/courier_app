namespace SwiftParcel.ExternalAPI.Lecturer.Application.DTO
{
    public class OfferRequestStatusDto
    {
        public Guid OfferId { get; set; }
        public bool IsReady { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
