using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using SwiftParcel.Services.Availability.Application.Commands;

namespace SwiftParcel.Services.Availability.Application.Events.External.Handlers
{
    public class CourierDeletedHandler  : IEventHandler<CourierDeleted>
    {
        private readonly ICommandDispatcher _dispatcher;

        public CourierDeletedHandler(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public Task HandleAsync(CourierDeleted @event, CancellationToken cancellationToken) => _dispatcher.SendAsync(new DeleteResource(@event.CourierId));
    }
}