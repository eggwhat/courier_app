using Convey.Types;
using SwiftParcel.ExternalAPI.Baronomat.Core.Entities;

namespace SwiftParcel.ExternalAPI.Baronomat.Infrastructure.Mongo.Documents
{
    public class OrderSnippetDocument: IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public int OrderId { get; set; }
    }
}