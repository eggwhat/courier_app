using Convey.Types;

namespace SwiftParcel.Services.Orders.Infrastructure.Mongo.Documents
{
    public class CustomerDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
    }
}
