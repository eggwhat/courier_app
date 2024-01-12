using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using StackExchange.Redis;
using SwiftParcel.ExternalAPI.Lecturer.Application;
using SwiftParcel.ExternalAPI.Lecturer.Application.DTO;
using SwiftParcel.ExternalAPI.Lecturer.Application.Queries;
using SwiftParcel.ExternalAPI.Lecturer.Application.Services.Clients;
using SwiftParcel.ExternalAPI.Lecturer.Core.Entities;
using SwiftParcel.ExternalAPI.Lecturer.Infrastructure.Mongo.Documents;

namespace SwiftParcel.ExternalAPI.Lecturer.Infrastructure.Mongo.Queries.Handlers
{
    public class GetOrdersHandler : IQueryHandler<GetOrders, IEnumerable<OrderDto>>
    {
        private readonly IMongoRepository<OfferSnippetDocument, Guid> _offerSnippetRepository;
        private readonly IIdentityManagerServiceClient _identityManagerServiceClient;
        private readonly IOffersServiceClient _offersServiceClient;
        private readonly IAppContext _appContext;

        public GetOrdersHandler(IMongoRepository<OfferSnippetDocument, Guid> offerSnippetRepository,
            IIdentityManagerServiceClient identityManagerServiceClient,
            IOffersServiceClient offersServiceClient, IAppContext appContext)
        {
            _offerSnippetRepository = offerSnippetRepository;
            _identityManagerServiceClient = identityManagerServiceClient;
            _offersServiceClient = offersServiceClient;
            _appContext = appContext;
        }

        public async Task<IEnumerable<OrderDto>> HandleAsync(GetOrders query, CancellationToken cancellationToken)
        {
            var documents = _offerSnippetRepository.Collection.AsQueryable();
            var identity = _appContext.Identity;
            if (identity.IsAuthenticated && identity.Id != query.CustomerId)
            {
                return Enumerable.Empty<OrderDto>();
            }

            documents = documents.Where(p => p.CustomerId == query.CustomerId && p.Status == OfferSnippetStatus.Confirmed);
            var token = await _identityManagerServiceClient.GetToken();
            var orders = new List<OrderDto>();
            foreach(var offerSnippet in documents)
            {
                var response = await _offersServiceClient.GetOfferAsync(token, offerSnippet.OfferId.ToString());
                if(response == null || response.Result == null)
                    continue;
                var order = new OrderDto(response.Result, query.CustomerId, offerSnippet.Status.ToString(),
                    Company.MiniCurrier.ToString());
                orders.Add(order);
            }
            return orders;
        }
    }
}