namespace SwiftParcel.ExternalAPI.Lecturer.Core.Entities
{
    public class OfferSnippet
    {
        public Guid OfferRequestId { get; set; }
        public Guid? OfferId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime ValidTo { get; set; }
        public OfferSnippetStatus Status { get; set; }

        public OfferSnippet(Guid offerRequestId, Guid? offerId, Guid customerId, DateTime validTo, OfferSnippetStatus status)
        {
            OfferRequestId = offerRequestId;
            OfferId = offerId;
            CustomerId = customerId;
            ValidTo = validTo;
            Status = status;
        }
        public void Accept(Guid offerId)
        {
            OfferId = offerId;
            Status = OfferSnippetStatus.Approved;
        }

        public void Confirm()
        {
            Status = OfferSnippetStatus.Confirmed;
        }

        public void Cancel()
        {
            Status = OfferSnippetStatus.Cancelled;
        }
    }
}