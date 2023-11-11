using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Availability.Core.Events;

namespace SwiftParcel.Services.Availability.Core.Entities
{
    public class AggregateRoot
    {
        private readonly List<IDomainEvent> _events = new List<IDomainEvent>();
        public IEnumerable<IDomainEvent> Events => _events;
        public AggregateId Id {get; protected set;}
        public int Version {get; protected set;}

        // here 'event' is not an C# event, but verbatim
        protected void AggEvent(IDomainEvent @event)
        {
            if(!_events.Any())
            {
                Version++;
            }

            _events.Add(@event);
        }

        public void ClearEvents() => _events.Clear();
    }
}