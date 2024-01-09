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
using SwiftParcel.Services.Orders.Core.Entities;
using SwiftParcel.Services.Orders.Application.Services;
using SwiftParcel.Services.Orders.Application.Services.Clients;

namespace SwiftParcel.Services.Orders.Infrastructure.Mongo.QueriesHandlers
{
    public class GetOrdersRequestsHandler : IQueryHandler<GetOrdersRequests, IEnumerable<OrderDto>>
    {
        private readonly IMongoRepository<OrderDocument, Guid> _orderRepository;
        private readonly ILecturerApiServiceClient _lecturerApiServiceClient;
        private readonly IAppContext _appContext;
        private readonly IDateTimeProvider _dateTimeProvider;

        public GetOrdersRequestsHandler(IMongoRepository<OrderDocument, Guid> orderRepository, 
            ILecturerApiServiceClient lecturerApiServiceClient, IAppContext appContext, IDateTimeProvider dateTimeProvider)
        {
            _orderRepository = orderRepository;
            _lecturerApiServiceClient = lecturerApiServiceClient;
            _appContext = appContext;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<IEnumerable<OrderDto>> HandleAsync(GetOrdersRequests query, CancellationToken cancellationToken)
        {
            var documents = _orderRepository.Collection.AsQueryable();
            var identity = _appContext.Identity;
            if (identity.IsAuthenticated && identity.Id != query.CustomerId && !identity.IsOfficeWorker)
            {
                return Enumerable.Empty<OrderDto>();
            }

            documents = documents.Where(p => p.CustomerId == query.CustomerId);

            documents = documents.Where(p => (p.Status == OrderStatus.WaitingForDecision || p.Status == OrderStatus.Approved)
                && p.RequestValidTo >= _dateTimeProvider.Now);
            var orders = await documents.ToListAsync();
            var ordersDto = orders.Select(p => p.AsDto());

            var miniCurrierOrders = await _lecturerApiServiceClient.GetOrderRequestsAsync(query.CustomerId.ToString());
            ordersDto = ordersDto.Concat(miniCurrierOrders);

            return ordersDto;
        }
    }
}