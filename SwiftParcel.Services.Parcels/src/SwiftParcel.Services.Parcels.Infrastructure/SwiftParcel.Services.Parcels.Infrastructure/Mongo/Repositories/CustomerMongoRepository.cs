using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey.Persistence.MongoDB;
using SwiftParcel.Services.Parcels.Core.Entities;
using SwiftParcel.Services.Parcels.Core.Repositories;
using SwiftParcel.Services.Parcels.Infrastructure.Mongo.Documents;

namespace SwiftParcel.Services.Parcels.Infrastructure.Mongo.Repositories
{
    public class CustomerMongoRepository : ICustomerRepository
    {
        private readonly IMongoRepository<CustomerDocument, Guid> _repository;

        public CustomerMongoRepository(IMongoRepository<CustomerDocument, Guid> repository)
            => _repository = repository;

        public Task<bool> ExistsAsync(Guid id)
            => _repository.ExistsAsync(c => c.Id == id);

        public Task AddAsync(Customer customer)
            => _repository.AddAsync(customer.AsDocument());
    }
}
