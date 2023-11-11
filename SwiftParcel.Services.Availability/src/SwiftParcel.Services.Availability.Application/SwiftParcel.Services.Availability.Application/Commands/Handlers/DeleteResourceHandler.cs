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
    public class DeleteResourceHandler: ICommandHandler<DeleteResource>
    {
        private readonly IResourcesRepository _repository;
        private readonly IEventProcessor _eventProcessor;

        public DeleteResourceHandler(IResourcesRepository repository, IEventProcessor eventProcessor)
        {
            _repository = repository;
            _eventProcessor = eventProcessor;
        }
        
        public async Task HandleAsync(DeleteResource command, CancellationToken cancellationToken)
        {
            var resource = await _repository.GetAsync(command.ResourceId);
            
            if (resource is null)
            {
                throw new ResourceNotFoundException(command.ResourceId);
            }

            resource.Delete();
            await _repository.DeleteAsync(resource.Id);
            await _eventProcessor.ProcessAsync(resource.Events);
        }
    }
}