using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.Persistence.MongoDB;
using SwiftParcel.Services.Couriers.Core.Entities;
using SwiftParcel.Services.Couriers.Core.Repositories;

namespace SwiftParcel.Services.Couriers.Infrastructure.Mongo.Repositories
{
    internal class CouriersMongoRepository : ICourierRepository
    {
        private readonly IMongoRepository<CourierDocument, Guid> _repository;

        public CouriersMongoRepository(IMongoRepository<CourierDocument, Guid> repository)
            => _repository = repository;

        public Task<Courier> GetAsync(Guid id)
            => _repository
                .GetAsync(id)
                .AsEntityAsync();

        public Task AddAsync(Courier courier)
            => _repository.AddAsync(courier.AsDocument());

        public Task UpdateAsync(Courier courier)
            => _repository.UpdateAsync(courier.AsDocument());

        public Task DeleteAsync(Courier courier)
            => _repository.DeleteAsync(courier.Id);
    }
}