using Convey.Persistence.MongoDB;
using SwiftParcel.ExternalAPI.Baronomat.Infrastructure.Mongo.Documents;
using SwiftParcel.ExternalAPI.Baronomat.Core.Repositories;
using SwiftParcel.ExternalAPI.Baronomat.Core.Entities;

namespace SwiftParcel.ExternalAPI.Baronomat.Infrastructure.Mongo.Repositories
{
    internal class OfferSnippetMongoRepository: IOfferSnippetRepository
    {
        private readonly IMongoRepository<OfferSnippetDocument, Guid> _repository;

        public OfferSnippetMongoRepository(IMongoRepository<OfferSnippetDocument, Guid> repository)
            => _repository = repository;

        public Task<OfferSnippet> GetAsync(Guid id)
            => _repository
                .GetAsync(id)
                .AsEntityAsync();

        public Task AddAsync(OfferSnippet offerSnippet)
            => _repository.AddAsync(offerSnippet.AsDocument());

        public Task UpdateAsync(OfferSnippet offerSnippet) 
            => _repository.UpdateAsync(offerSnippet.AsDocument());
    }
}