using System;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using SwiftParcel.Services.Orders.Application.DTO;
using SwiftParcel.Services.Orders.Application.Queries;
using SwiftParcel.Services.Orders.Infrastructure.Mongo.Documents;

namespace SwiftParcel.Services.Orders.Infrastructure.Mongo.QueriesHandlers
{
    public class GetOrderHandler : IQueryHandler<GetOrder, OrderDto>
    {
        private readonly IMongoRepository<OrderDocument, Guid> _orderRepository;

        public GetOrderHandler(IMongoRepository<OrderDocument, Guid> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto> HandleAsync(GetOrder query)
        {
            var document = await _orderRepository.GetAsync(p => p.Id == query.OrderId);

            return document?.AsDto();
        }
    }
}