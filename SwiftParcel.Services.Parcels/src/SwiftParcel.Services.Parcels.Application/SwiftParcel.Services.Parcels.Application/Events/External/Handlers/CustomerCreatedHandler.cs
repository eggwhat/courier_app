using Convey.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwiftParcel.Services.Parcels.Application.Exceptions;
using SwiftParcel.Services.Parcels.Core.Entities;
using SwiftParcel.Services.Parcels.Core.Repositories;

namespace SwiftParcel.Services.Parcels.Application.Events.External.Handlers
{
    public class CustomerCreatedHandler : IEventHandler<CustomerCreated>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerCreatedHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task HandleAsync(CustomerCreated @event, CancellationToken cancellationToken)
        {
            if (await _customerRepository.ExistsAsync(@event.CustomerId))
            {
                throw new CustomerAlreadyAddedException(@event.CustomerId);
            }

            await _customerRepository.AddAsync(new Customer(@event.CustomerId));
        }
    }
}
