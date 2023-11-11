using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using SwiftParcel.Services.Availability.Application.Exceptions;
using SwiftParcel.Services.Availability.Application.Services;
using SwiftParcel.Services.Availability.Application.Services.Clients;
using SwiftParcel.Services.Availability.Core.Repositories;

namespace SwiftParcel.Services.Availability.Application.Commands.Handlers
{
    public class ReserveResourceHandler : ICommandHandler<ReserveResource>
    {
        private readonly IResourcesRepository _repository;
        private readonly ICustomersServiceClient _customersServiceClient;
        private readonly IEventProcessor _eventProcessor;
        private readonly IAppContext _appContext;

        public ReserveResourceHandler(IResourcesRepository repository, ICustomersServiceClient customersServiceClient,
            IEventProcessor eventProcessor, IAppContext appContext)
        {
            _repository = repository;
            _customersServiceClient = customersServiceClient;
            _eventProcessor = eventProcessor;
            _appContext = appContext;
        }

        public async Task HandleAsync(ReserveResource command)
        {
            var identity = _appContext.Identity;
            if (identity.IsAuthenticated && identity.Id != command.CustomerId && !identity.IsAdmin)
            {
                throw new UnauthorizedResourceAccessException(command.ResourceId, identity.Id);
            }

            var resource = await _repository.GetAsync(command.ResourceId);
            if (resource is null)
            {
                throw new ResourceNotFoundException(command.ResourceId);
            }

            var customerState = await _customersServiceClient.GetStateAsync(command.CustomerId);
            if (customerState is null)
            {
                throw new CustomerNotFoundException(command.CustomerId);
            }

            if (!customerState.IsValid)
            {
                throw new InvalidCustomerStateException(command.ResourceId, customerState?.State);
            }

            var reservation = new Reservation(command.DateTime, command.Priority);
            resource.AddReservation(reservation);
            await _repository.UpdateAsync(resource);
            await _eventProcessor.ProcessAsync(resource.Events);
        }
    }
}