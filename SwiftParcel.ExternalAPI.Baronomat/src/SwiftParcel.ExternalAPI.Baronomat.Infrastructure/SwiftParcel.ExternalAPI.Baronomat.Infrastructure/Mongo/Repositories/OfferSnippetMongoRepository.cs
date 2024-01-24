using Convey.Persistence.MongoDB;
using SwiftParcel.ExternalAPI.Baronomat.Infrastructure.Mongo.Documents;
using SwiftParcel.ExternalAPI.Baronomat.Core.Repositories;
using SwiftParcel.ExternalAPI.Baronomat.Core.Entities;

namespace SwiftParcel.ExternalAPI.Baronomat.Infrastructure.Mongo.Repositories
{
    internal class OfferSnippetMongoRepository: IOfferSnippetRepository
    {
        private readonly IMongoRepository<OrderSnippetDocument, Guid> _repository;

        public OfferSnippetMongoRepository(IMongoRepository<OrderSnippetDocument, Guid> repository)
            => _repository = repository;

        public Task<OrderSnippet> GetAsync(Guid id)
            => _repository
                .GetAsync(id)
                .AsEntityAsync();

        public Task AddAsync(OrderSnippet offerSnippet)
            => _repository.AddAsync(offerSnippet.AsDocument());

        public Task UpdateAsync(OrderSnippet offerSnippet) 
            => _repository.UpdateAsync(offerSnippet.AsDocument());
    }
}