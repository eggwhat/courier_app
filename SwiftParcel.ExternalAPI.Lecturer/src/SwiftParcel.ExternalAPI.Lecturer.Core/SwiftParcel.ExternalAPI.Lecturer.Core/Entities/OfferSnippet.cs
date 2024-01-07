namespace SwiftParcel.ExternalAPI.Lecturer.Core.Entities
{
    public class OfferSnippet
    {
        public Guid OfferRequestId { get; set; }
        public Guid? OfferId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime ValidTo { get; set; }
        public OfferStatus Status { get; set; }

        public OfferSnippet(Guid offerRequestId, Guid? offerId, Guid customerId, DateTime validTo, OfferStatus status)
        {
            OfferRequestId = offerRequestId;
            OfferId = offerId;
            CustomerId = customerId;
            ValidTo = validTo;
            Status = status;
        }
    }
}