using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using SwiftParcel.Services.Customers.Application.Exceptions;
using SwiftParcel.Services.Customers.Application.Services;
using SwiftParcel.Services.Customers.Core.Exceptions;
using SwiftParcel.Services.Customers.Core.Repositories;

namespace SwiftParcel.Services.Customers.Application.Commands.Handlers
{
    public class CompleteCustomerRegistrationHandler : ICommandHandler<CompleteCustomerRegistration>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEventMapper _eventMapper;
        private readonly IMessageBroker _messageBroker;
        
        public CompleteCustomerRegistrationHandler(ICustomerRepository customerRepository, IEventMapper eventMapper,
            IMessageBroker messageBroker)
        {
            _customerRepository = customerRepository;
            _eventMapper = eventMapper;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(CompleteCustomerRegistration command, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetAsync(command.CustomerId);
            if (customer is null)
            {
                throw new CustomerNotFoundException(command.CustomerId);
            }
            
            if (customer.State is Core.Entities.State.Valid)
            {
                throw new CustomerAlreadyRegisteredException(command.CustomerId);
            }

            customer.CompleteRegistration(command.FirstName, command.LastName, command.Address, command.SourceAddress);
            await _customerRepository.UpdateAsync(customer);

            var events = _eventMapper.MapAll(customer.Events);
            await _messageBroker.PublishAsync(events.ToArray());
        }
    }
}