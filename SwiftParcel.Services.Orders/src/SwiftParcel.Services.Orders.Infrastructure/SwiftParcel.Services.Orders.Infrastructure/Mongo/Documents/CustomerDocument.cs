using Convey.Types;

namespace SwiftParcel.Services.Orders.Infrastructure.Mongo.Documents
{
    public class CustomerDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}
