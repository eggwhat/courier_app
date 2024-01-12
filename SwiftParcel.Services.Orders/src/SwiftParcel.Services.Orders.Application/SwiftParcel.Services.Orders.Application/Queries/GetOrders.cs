using Convey.CQRS.Queries;
using SwiftParcel.Services.Orders.Application.DTO;

namespace SwiftParcel.Services.Orders.Application.Queries
{
    public class GetOrders : IQuery<IEnumerable<OrderDto>>
    {
        public Guid CustomerId { get; set; }
    }
}