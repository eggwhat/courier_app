using System;
using Convey.CQRS.Queries;
using SwiftParcel.Services.Orders.Application.DTO;

namespace SwiftParcel.Services.Orders.Application.Queries
{
    public class GetOrder : IQuery<OrderDto>
    {
        public Guid OrderId { get; set; }
    }
}