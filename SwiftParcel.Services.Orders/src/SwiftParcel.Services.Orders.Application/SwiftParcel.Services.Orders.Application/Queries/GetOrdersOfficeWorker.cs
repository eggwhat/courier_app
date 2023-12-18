using Convey.CQRS.Queries;
using SwiftParcel.Services.Orders.Application.DTO;

namespace SwiftParcel.Services.Orders.Application.Queries
{
    public class GetOrdersOfficeWorker : IQuery<IEnumerable<OrderDto>>
    {
    }
}