using Convey.CQRS.Queries;
using SwiftParcel.ExternalAPI.Baronomat.Application.DTO;

namespace SwiftParcel.ExternalAPI.Baronomat.Application.Queries
{
    public class GetOrders : IQuery<IEnumerable<OrderDto>>
    {
        public Guid CustomerId { get; set; }
    }
}
