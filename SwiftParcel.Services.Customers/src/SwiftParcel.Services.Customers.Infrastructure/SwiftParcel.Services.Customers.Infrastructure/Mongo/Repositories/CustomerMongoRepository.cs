using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.Persistence.MongoDB;
using SwiftParcel.Services.Customers.Core.Entities;
using SwiftParcel.Services.Customers.Core.Repositories;
using SwiftParcel.Services.Customers.Infrastructure.Mongo.Documents;

namespace SwiftParcel.Services.Customers.Infrastructure.Mongo.Repositories
{
    public class CustomerMongoRepository : ICustomerRepository
    {
        private readonly IMongoRepository<CustomerDocument, Guid> _repository;

        public CustomerMongoRepository(IMongoRepository<CustomerDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<Customer> GetAsync(Guid id)
        {
            var customer = await _repository.GetAsync(o => o.Id == id);

            return customer?.AsEntity();
        }

        public Task AddAsync(Customer customer) => _repository.AddAsync(customer.AsDocument());
        public Task UpdateAsync(Customer customer) => _repository.UpdateAsync(customer.AsDocument());
    }
}