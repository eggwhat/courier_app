

using SwiftParcel.ExternalAPI.Baronomat.Application.DTO;
using SwiftParcel.ExternalAPI.Baronomat.Core.Entities;

namespace SwiftParcel.ExternalAPI.Baronomat.Infrastructure.Mongo.Documents
{
    internal static class Extensions
    {
        public static InquiryOffer AsEntity(this InquiryOfferDocument document)
            => document is null? null : new InquiryOffer(
                document.Id,
                document.TotalPrice,
                document.ExpiringAt,
                document.PriceBreakDown
                );

        public static async Task<InquiryOffer> AsEntityAsync(this Task<InquiryOfferDocument> task)
            => (await task).AsEntity();

        public static InquiryOfferDocument AsDocument(this InquiryOffer entity)
            => new InquiryOfferDocument
            {
                Id = entity.ParcelId,
                TotalPrice = entity.TotalPrice,
                ExpiringAt = entity.ExpiringAt,
                PriceBreakDown = entity.PriceBreakDown
            };
        
        public static async Task<InquiryOfferDocument> AsDocumentAsync(this Task<InquiryOffer> task)
            => (await task).AsDocument();

        public static List<PriceBreakDownItemDto> AsDto(this List<PriceBreakDownItem> entity)
        {
            var dto = new List<PriceBreakDownItemDto>();
            foreach (var item in entity)
            {
                dto.Add(new PriceBreakDownItemDto()
                {
                    Amount = item.Amount,
                    Currency = item.Currency,
                    Description = item.Description
                });
            }
            return dto;
        }


        public static OrderSnippet AsEntity(this OrderSnippetDocument document)
            => document is null? null : new OrderSnippet(
                document.Id,
                document.OrderId
                );

        public static async Task<OrderSnippet> AsEntityAsync(this Task<OrderSnippetDocument> task)
            => (await task).AsEntity();

        public static OrderSnippetDocument AsDocument(this OrderSnippet entity)
            => new OrderSnippetDocument
            {
                Id = entity.CustomerId,
                OrderId = entity.OrderId
            };
        
        public static async Task<OrderSnippetDocument> AsDocumentAsync(this Task<OrderSnippet> task)
            => (await task).AsDocument();
    }
}
