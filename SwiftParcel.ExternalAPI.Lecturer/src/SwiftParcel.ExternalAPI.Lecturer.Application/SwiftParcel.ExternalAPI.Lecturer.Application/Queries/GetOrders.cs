using Convey.CQRS.Queries;
using SwiftParcel.ExternalAPI.Lecturer.Application.DTO;

namespace SwiftParcel.ExternalAPI.Lecturer.Application.Queries
{
    public class GetOrders : IQuery<IEnumerable<OrderDto>>
    {
        public Guid CustomerId { get; set; }
    }
}
