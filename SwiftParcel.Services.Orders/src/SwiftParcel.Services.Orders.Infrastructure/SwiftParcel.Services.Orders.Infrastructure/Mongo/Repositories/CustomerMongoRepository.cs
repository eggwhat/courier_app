using System;
using System.Threading.Tasks;
using Convey.Persistence.MongoDB;
using SwiftParcel.Services.Orders.Core.Entities;
using SwiftParcel.Services.Orders.Core.Repositories;
using SwiftParcel.Services.Orders.Infrastructure.Mongo.Documents;

namespace SwiftParcel.Services.Orders.Infrastructure.Mongo.Repositories
{
    public class CustomerMongoRepository : ICustomerRepository
    {
        private readonly IMongoRepository<CustomerDocument, Guid> _repository;

        public CustomerMongoRepository(IMongoRepository<CustomerDocument, Guid> repository)
        {
            _repository = repository;
        }

        public Task<bool> ExistsAsync(Guid id) => _repository.ExistsAsync(c => c.Id == id);
        public Task AddAsync(Customer customer) => _repository.AddAsync(customer.AsDocument());
    }
}