using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Availability.Core.Events;

namespace SwiftParcel.Services.Availability.Application.Events
{
    public interface IDomainEventHandler<in T> where T : class, IDomainEvent
    {
        Task HandleAsync(T @event);
    }
}