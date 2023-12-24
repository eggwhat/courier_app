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
    public class GetCustomerHandler : IQueryHandler<GetCustomer, CustomerDetailsDto>
    {
        private readonly IMongoRepository<CustomerDocument, Guid> _customerRepository;

        public GetCustomerHandler(IMongoRepository<CustomerDocument, Guid> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDetailsDto> HandleAsync(GetCustomer query, CancellationToken cancellationToken)
        {
            var document = await _customerRepository.GetAsync(p => p.Id == query.CustomerId);

            return document?.AsDetailsDto();
        }
    }
}