using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chronicle;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using SwiftParcel.Services.OrdersCreator.Commands;
using SwiftParcel.Services.OrdersCreator.Events.External;

namespace SwiftParcel.Services.OrdersCreator.Handlers
{
    public class AIOrderMakingHandler : 
        ICommandHandler<MakeOrder>, 
        IEventHandler<OrderApproved>, 
        IEventHandler<OrderCreated>, 
        IEventHandler<ParcelAddedToOrder>,
        IEventHandler<CourierAssignedToOrder>,
        IEventHandler<ResourceReserved>
    {
        private readonly ISagaCoordinator _coordinator;

        public AIOrderMakingHandler(ISagaCoordinator coordinator)
        {
            _coordinator = coordinator;
        }
        
        public Task HandleAsync(MakeOrder command, CancellationToken cancellationToken)
            => _coordinator.ProcessAsync(command, SagaContext.Empty);

        public Task HandleAsync(OrderApproved @event, CancellationToken cancellationToken)
            => _coordinator.ProcessAsync(@event, SagaContext.Empty);

        public Task HandleAsync(OrderCreated @event, CancellationToken cancellationToken)
            => _coordinator.ProcessAsync(@event, SagaContext.Empty);
        
        public Task HandleAsync(ParcelAddedToOrder @event, CancellationToken cancellationToken)
            => _coordinator.ProcessAsync(@event, SagaContext.Empty);
        
        public Task HandleAsync(CourierAssignedToOrder @event, CancellationToken cancellationToken)
            => _coordinator.ProcessAsync(@event, SagaContext.Empty);

        public Task HandleAsync(ResourceReserved @event, CancellationToken cancellationToken)
            => _coordinator.ProcessAsync(@event, SagaContext.Empty);
    }
}