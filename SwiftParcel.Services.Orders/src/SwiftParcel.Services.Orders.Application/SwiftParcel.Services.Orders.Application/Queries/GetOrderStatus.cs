using Convey.CQRS.Queries;
using SwiftParcel.Services.Orders.Application.DTO;

namespace SwiftParcel.Services.Orders.Application.Queries
{
    public class GetOrderStatus : IQuery<OrderStatusDto>
    {
        public Guid OrderId { get; set; }
    }
}