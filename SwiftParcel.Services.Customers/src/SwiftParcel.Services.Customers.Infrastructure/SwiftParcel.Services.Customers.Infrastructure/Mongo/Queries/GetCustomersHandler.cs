using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using SwiftParcel.Services.Customers.Application.Dto;
using SwiftParcel.Services.Customers.Application.Queries;
using SwiftParcel.Services.Customers.Infrastructure.Mongo.Documents;

namespace SwiftParcel.Services.Customers.Infrastructure.Mongo.Queries
{
    public class GetCustomersHandler : IQueryHandler<GetCustomers, IEnumerable<CustomerDto>>
    {
        private readonly IMongoRepository<CustomerDocument, Guid> _customerRepository;

        public GetCustomersHandler(IMongoRepository<CustomerDocument, Guid> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerDto>> HandleAsync(GetCustomers query, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.FindAsync(_ => true);

            return customers.Select(p => p.AsDto());
        }
    }
}