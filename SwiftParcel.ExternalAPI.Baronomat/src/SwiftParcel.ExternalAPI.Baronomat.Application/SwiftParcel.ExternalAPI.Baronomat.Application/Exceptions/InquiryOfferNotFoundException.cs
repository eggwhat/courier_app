namespace SwiftParcel.ExternalAPI.Baronomat.Application.Exceptions
{
    public class InquiryOfferNotFoundException : AppException
    {
        public override string Code { get; } = "inqury_offer_not_found";
        public Guid InquiryOfferId { get; }
        public InquiryOfferNotFoundException(Guid inquiryOfferId) : base("inqury_offer_not_found")
        {
            InquiryOfferId = inquiryOfferId;
        }
    }
}