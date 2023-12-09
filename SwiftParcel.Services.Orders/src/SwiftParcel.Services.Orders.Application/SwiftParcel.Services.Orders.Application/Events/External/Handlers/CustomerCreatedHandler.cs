using System.Threading.Tasks;
using Convey.CQRS.Events;
using SwiftParcel.Services.Orders.Application.Exceptions;
using SwiftParcel.Services.Orders.Core.Entities;
using SwiftParcel.Services.Orders.Core.Repositories;

namespace SwiftParcel.Services.Orders.Application.Events.External.Handlers
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

            await _customerRepository.AddAsync(new Customer(@event.CustomerId, @event.Email, @event.FullName));
        }
    }
}