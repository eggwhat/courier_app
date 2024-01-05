

using SwiftParcel.ExternalAPI.Lecturer.Core.Entities;

namespace SwiftParcel.ExternalAPI.Lecturer.Infrastructure.Mongo.Documents
{
    internal static class Extensions
    {
        public static InquiryOffer AsEntity(this InquiryOfferDocument document)
            => document is null? null : new InquiryOffer(
                document.Id,
                document.InquiryId,
                document.TotalPrice,
                document.ExpiringAt
                );

        public static async Task<InquiryOffer> AsEntityAsync(this Task<InquiryOfferDocument> task)
            => (await task).AsEntity();

        public static InquiryOfferDocument AsDocument(this InquiryOffer entity)
            => new InquiryOfferDocument
            {
                Id = entity.ParcelId,
                InquiryId = entity.InquiryId,
                TotalPrice = entity.TotalPrice,
                ExpiringAt = entity.ExpiringAt
            };
        
        public static async Task<InquiryOfferDocument> AsDocumentAsync(this Task<InquiryOffer> task)
            => (await task).AsDocument();

    }
}
