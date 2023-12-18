using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Elasticsearch.Net;
using SwiftParcel.Services.Orders.Application.DTO;
using SwiftParcel.Services.Orders.Application.Queries;
using SwiftParcel.Services.Orders.Infrastructure.Mongo.Documents;


namespace SwiftParcel.Services.Orders.Infrastructure.Mongo.QueriesHandlers
{
    public class GetOrderStatusHandler : IQueryHandler<GetOrderStatus, OrderStatusDto>
    {
        private readonly IMongoRepository<OrderDocument, Guid> _orderRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public GetOrderStatusHandler(IMongoRepository<OrderDocument, Guid> orderRepository,
            IDateTimeProvider dateTimeProvider)
        {
            _orderRepository = orderRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<OrderStatusDto> HandleAsync(GetOrderStatus query, CancellationToken cancellationToken)
        {
            var document = await _orderRepository.GetAsync(p => p.Id == query.OrderId);
            var orderStatus = new OrderStatusDto
            {
                OrderId = document.Id,
                Status = document.Status.ToString(),
                TimeStamp = _dateTimeProvider.Now()
            };
            return orderStatus;
        }
    }
}