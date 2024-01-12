using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SwiftParcel.ExternalAPI.Lecturer.Application;
using SwiftParcel.ExternalAPI.Lecturer.Application.DTO;
using SwiftParcel.ExternalAPI.Lecturer.Application.Queries;
using SwiftParcel.ExternalAPI.Lecturer.Application.Services.Clients;
using SwiftParcel.ExternalAPI.Lecturer.Core.Entities;
using SwiftParcel.ExternalAPI.Lecturer.Infrastructure.Mongo.Documents;

namespace SwiftParcel.ExternalAPI.Lecturer.Infrastructure.Mongo.Queries.Handlers
{
    public class GetOrdersRequestHandler : IQueryHandler<GetOrderRequests, IEnumerable<OrderDto>>
    {
        private readonly IMongoRepository<OfferSnippetDocument, Guid> _repository;
        private readonly IAppContext _appContext;
        private readonly IIdentityManagerServiceClient _identityManagerServiceClient;
        private readonly IOffersServiceClient _offersServiceClient;

        public GetOrdersRequestHandler(IMongoRepository<OfferSnippetDocument, Guid> repository,
            IAppContext appContext, IIdentityManagerServiceClient identityManagerServiceClient,
            IOffersServiceClient offersServiceClient)
        {
            _repository = repository;
            _appContext = appContext;
            _identityManagerServiceClient = identityManagerServiceClient;
            _offersServiceClient = offersServiceClient;
        }

        public async Task<IEnumerable<OrderDto>> HandleAsync(GetOrderRequests query, CancellationToken cancellationToken)
        {
            var documents = _repository.Collection.AsQueryable();
            var identity = _appContext.Identity;
            if (identity.IsAuthenticated && identity.Id != query.CustomerId)
            {
                return Enumerable.Empty<OrderDto>();
            }

            documents = documents.Where(p => p.CustomerId == query.CustomerId 
                && p.Status != OfferSnippetStatus.Confirmed && p.Status != OfferSnippetStatus.Cancelled);

            var token = await _identityManagerServiceClient.GetToken();
            var offerSnippetsUpdated = new List<OfferSnippet>();
            foreach (var offer in documents)
            {
                var offerSnippet = offer.AsEntity();
                if(offerSnippet.Status == OfferSnippetStatus.Approved)
                {
                    offerSnippetsUpdated.Add(offerSnippet);
                    continue;
                }
                var response = await _offersServiceClient.GetOfferRequestStatus(token, offer.Id.ToString());
                if(response == null || response.Result == null || response.Result.IsReady == false)
                {
                    offerSnippetsUpdated.Add(offerSnippet);
                    continue;
                }
                offerSnippet.Accept(response.Result.OfferId);
                offerSnippetsUpdated.Add(offerSnippet);
                await _repository.UpdateAsync(offerSnippet.AsDocument());
            }
            return offerSnippetsUpdated.Select(p 
                => new OrderDto(p.OfferRequestId, p.CustomerId,
                p.Status.ToString(), p.ValidTo, Company.MiniCurrier.ToString()));
        }
    }
}   