using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using SwiftParcel.Services.Availability.Application.Exceptions;
using SwiftParcel.Services.Availability.Application.Services;
using SwiftParcel.Services.Availability.Core.Repositories;

namespace SwiftParcel.Services.Availability.Application.Commands.Handlers
{
    public class ReleaseResourceReservationHandler : ICommandHandler<ReleaseResourceReservation>
    {
        private readonly IResourcesRepository _repository;
        private readonly IEventProcessor _eventProcessor;

        public ReleaseResourceReservationHandler(IResourcesRepository repository, IEventProcessor eventProcessor)
        {
            _repository = repository;
            _eventProcessor = eventProcessor;
        }
        
        public async Task HandleAsync(ReleaseResourceReservation command, CancellationToken cancellationToken)
        {
            var resource = await _repository.GetAsync(command.ResourceId);
            
            if (resource is null)
            {
                throw new ResourceNotFoundException(command.ResourceId);
            }

            var reservation = resource.Reservations.FirstOrDefault(r => r.DateTime.Date == command.DateTime.Date);
            resource.ReleaseReservation(reservation);
            await _repository.UpdateAsync(resource);
            await _eventProcessor.ProcessAsync(resource.Events);
        }
    }
}