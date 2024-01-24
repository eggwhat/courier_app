using SwiftParcel.ExternalAPI.Baronomat.Core.Entities;

namespace SwiftParcel.ExternalAPI.Baronomat.Application.Exceptions
{
    public class OfferNotApprovedException : AppException
    {
        public override string Code { get; } = "offer_not_approved";
        public Guid OfferId { get; }
        public OfferSnippetStatus Status { get; }

        public OfferNotApprovedException(Guid offerId, OfferSnippetStatus status) : base($"Offer with id: {offerId} is not approved; status={status}.")
        {
            OfferId = offerId;
            Status = status;
        }
    }
}