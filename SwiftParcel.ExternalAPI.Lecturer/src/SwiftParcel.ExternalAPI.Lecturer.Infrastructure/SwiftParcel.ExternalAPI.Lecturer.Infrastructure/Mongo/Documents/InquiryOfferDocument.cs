using Convey.Types;
using SwiftParcel.ExternalAPI.Lecturer.Core.Entities;

namespace SwiftParcel.ExternalAPI.Lecturer.Infrastructure.Mongo.Documents
{
    public class InquiryOfferDocument: IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid InquiryId { get; set; }
        public double TotalPrice { get; set; }
        public DateTime ExpiringAt { get; set; }
        public List<PriceBreakDownItem> PriceBreakDown { get; set; }
    }
}