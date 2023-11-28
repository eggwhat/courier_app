using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Convey.Persistence.MongoDB;
using SwiftParcel.Services.Parcels.Core.Entities;
using SwiftParcel.Services.Parcels.Core.Repositories;
using SwiftParcel.Services.Parcels.Infrastructure.Mongo.Documents;

namespace SwiftParcel.Services.Parcels.Infrastructure.Mongo.Repositories
{
    internal class ParcelMongoRepository : IParcelRepository
    {
        private readonly IMongoRepository<ParcelDocument, Guid> _repository;

        public ParcelMongoRepository(IMongoRepository<ParcelDocument, Guid> repository)
            => _repository = repository;

        public Task<Parcel> GetAsync(Guid id)
            => _repository
                .GetAsync(id)
                .AsEntityAsync();

        public Task AddAsync(Parcel parcel)
            => _repository.AddAsync(parcel.AsDocument());

        public Task UpdateAsync(Parcel parcel)
            => _repository.UpdateAsync(parcel.AsDocument());

        public Task DeleteAsync(Parcel parcel)
            => _repository.DeleteAsync(parcel.Id);
    }
}