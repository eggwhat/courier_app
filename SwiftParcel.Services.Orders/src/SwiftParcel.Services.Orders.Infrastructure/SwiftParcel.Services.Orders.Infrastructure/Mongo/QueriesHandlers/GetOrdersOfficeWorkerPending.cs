using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SwiftParcel.Services.Orders.Application;
using SwiftParcel.Services.Orders.Application.DTO;
using SwiftParcel.Services.Orders.Application.Queries;
using SwiftParcel.Services.Orders.Infrastructure.Mongo.Documents;
using SwiftParcel.Services.Orders.Core.Entities;
using SwiftParcel.Services.Orders.Application.Services;

namespace SwiftParcel.Services.Orders.Infrastructure.Mongo.QueriesHandlers
{
    public class GetOrdersOfficeWorkerPendingHandler: IQueryHandler<GetOrdersOfficeWorkerPending, IEnumerable<OrderDto>>
    {
        private readonly IMongoRepository<OrderDocument, Guid> _orderRepository;
        private readonly IAppContext _appContext;
        private readonly IDateTimeProvider _dateTimeProvider;

        public GetOrdersOfficeWorkerPendingHandler(IMongoRepository<OrderDocument, Guid> orderRepository, 
            IAppContext appContext, IDateTimeProvider dateTimeProvider)
        {
            _orderRepository = orderRepository;
            _appContext = appContext;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<IEnumerable<OrderDto>> HandleAsync(GetOrdersOfficeWorkerPending query, CancellationToken cancellationToken)
        {
            var documents = _orderRepository.Collection.AsQueryable();
            var identity = _appContext.Identity;
            if (!identity.IsOfficeWorker)
            {
                return Enumerable.Empty<OrderDto>();
            }
            
            documents = documents.Where(p => p.Status == OrderStatus.WaitingForDecision && p.RequestValidTo > _dateTimeProvider.Now);
            var orders = await documents.ToListAsync();

            return orders.Select(p => p.AsDto());
        }
    }
}