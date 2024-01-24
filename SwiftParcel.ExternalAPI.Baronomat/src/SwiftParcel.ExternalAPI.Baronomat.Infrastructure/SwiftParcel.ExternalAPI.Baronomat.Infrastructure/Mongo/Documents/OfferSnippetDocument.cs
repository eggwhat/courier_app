using Convey.Types;
using SwiftParcel.ExternalAPI.Baronomat.Core.Entities;

namespace SwiftParcel.ExternalAPI.Baronomat.Infrastructure.Mongo.Documents
{
    public class OfferSnippetDocument: IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid? OfferId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime ValidTo { get; set; }
        public OfferSnippetStatus Status { get; set; }
    }
}