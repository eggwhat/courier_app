using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SwiftParcel.Services.Orders.Application;
using SwiftParcel.Services.Orders.Application.DTO;
using SwiftParcel.Services.Orders.Application.Queries;
using SwiftParcel.Services.Orders.Infrastructure.Mongo.Documents;

namespace SwiftParcel.Services.Orders.Infrastructure.Mongo.QueriesHandlers
{
    public class GetOrdersOfficeWorkerHandler : IQueryHandler<GetOrdersOfficeWorker, IEnumerable<OrderDto>>
    {
        private readonly IMongoRepository<OrderDocument, Guid> _orderRepository;
        private readonly IAppContext _appContext;

        public GetOrdersOfficeWorkerHandler(IMongoRepository<OrderDocument, Guid> orderRepository, IAppContext appContext)
        {
            _orderRepository = orderRepository;
            _appContext = appContext;
        }

        public async Task<IEnumerable<OrderDto>> HandleAsync(GetOrdersOfficeWorker query, CancellationToken cancellationToken)
        {
            var documents = _orderRepository.Collection.AsQueryable();
            var identity = _appContext.Identity;
            if (!identity.IsOfficeWorker)
            {
                return Enumerable.Empty<OrderDto>();
            }

            var orders = await documents.ToListAsync();

            return orders.Select(p => p.AsDto());
        }
    }
}