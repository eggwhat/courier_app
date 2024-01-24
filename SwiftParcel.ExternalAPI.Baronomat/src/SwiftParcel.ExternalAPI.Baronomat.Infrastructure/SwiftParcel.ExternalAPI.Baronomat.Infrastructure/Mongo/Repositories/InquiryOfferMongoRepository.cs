using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Convey.Persistence.MongoDB;
using SwiftParcel.ExternalAPI.Baronomat.Infrastructure.Mongo.Documents;
using SwiftParcel.ExternalAPI.Baronomat.Core.Entities;
using SwiftParcel.ExternalAPI.Baronomat.Core.Repositories;

namespace SwiftParcel.ExternalAPI.Baronomat.Infrastructure.Mongo.Repositories
{
    internal class InquiryOfferMongoRepository : IInquiryOfferRepository
    {
        private readonly IMongoRepository<InquiryOfferDocument, Guid> _repository;

        public InquiryOfferMongoRepository(IMongoRepository<InquiryOfferDocument, Guid> repository)
            => _repository = repository;

        public Task<InquiryOffer> GetAsync(Guid id)
            => _repository
                .GetAsync(id)
                .AsEntityAsync();

        public Task AddAsync(InquiryOffer inquiryOffer)
            => _repository.AddAsync(inquiryOffer.AsDocument());

    }
}
