using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Convey.Persistence.MongoDB;
using SwiftParcel.ExternalAPI.Lecturer.Infrastructure.Mongo.Documents;
using SwiftParcel.ExternalAPI.Lecturer.Core.Entities;
using SwiftParcel.ExternalAPI.Lecturer.Core.Repositories;

namespace SwiftParcel.ExternalAPI.Lecturer.Infrastructure.Mongo.Repositories
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
