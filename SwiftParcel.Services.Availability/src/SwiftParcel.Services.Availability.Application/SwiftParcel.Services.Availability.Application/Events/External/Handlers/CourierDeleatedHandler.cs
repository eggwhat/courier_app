using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Availability.Application.Events.External.Handlers
{
    public class CourierDeleatedHandler  : IEventHandler<VehicleDeleted>
    {
        private readonly ICommandDispatcher _dispatcher;

        public CourierDeletedHandler(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public Task HandleAsync(CourierDeleted @event) => _dispatcher.SendAsync(new DeleteResource(@event.CourierId));
    }
}