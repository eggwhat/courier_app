using Convey.Types;
using SwiftParcel.ExternalAPI.Baronomat.Core.Entities;

namespace SwiftParcel.ExternalAPI.Baronomat.Infrastructure.Mongo.Documents
{
    public class InquiryOfferDocument: IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public double TotalPrice { get; set; }
        public DateTime ExpiringAt { get; set; }
        public List<PriceBreakDownItem> PriceBreakDown { get; set; }
    }
}