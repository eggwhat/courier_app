using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using StackExchange.Redis;
using SwiftParcel.ExternalAPI.Baronomat.Application;
using SwiftParcel.ExternalAPI.Baronomat.Application.DTO;
using SwiftParcel.ExternalAPI.Baronomat.Application.Queries;
using SwiftParcel.ExternalAPI.Baronomat.Application.Services.Clients;
using SwiftParcel.ExternalAPI.Baronomat.Core.Entities;
using SwiftParcel.ExternalAPI.Baronomat.Infrastructure.Mongo.Documents;

namespace SwiftParcel.ExternalAPI.Baronomat.Infrastructure.Mongo.Queries.Handlers
{
    public class GetOrdersHandler : IQueryHandler<GetOrders, IEnumerable<OrderDto>>
    {
        private readonly IMongoRepository<OrderSnippetDocument, Guid> _offerSnippetRepository;
        private readonly IOrdersServiceClient _offersServiceClient;
        private readonly IAppContext _appContext;

        public GetOrdersHandler(IMongoRepository<OrderSnippetDocument, Guid> offerSnippetRepository,
            IOrdersServiceClient offersServiceClient, IAppContext appContext)
        {
            _offerSnippetRepository = offerSnippetRepository;
            _offersServiceClient = offersServiceClient;
            _appContext = appContext;
        }

        public async Task<IEnumerable<OrderDto>> HandleAsync(GetOrders query, CancellationToken cancellationToken)
        {
            var documents = _offerSnippetRepository.Collection.AsQueryable();

            documents = documents.Where(p => p.Id == query.CustomerId);
            var orders = new List<OrderDto>();
            foreach(var offerSnippet in documents)
            {
                var response = await _offersServiceClient.GetOrderAsync(offerSnippet.OrderId.ToString());
                if(response == null || response.Result == null)
                    continue;
                var order = new OrderDto(response.Result, query.CustomerId);
                orders.Add(order);
            }
            return orders;
        }
    }
}