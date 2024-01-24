namespace SwiftParcel.ExternalAPI.Baronomat.Application.Exceptions
{
    public class OfferNotFoundException : AppException
    {
        public override string Code { get; } = "offer_not_found";
        public Guid OfferId { get; }

        public OfferNotFoundException(Guid offerId) : base($"Offer with id: '{offerId}' was not found.")
        {
            OfferId = offerId;
        }
    }
}