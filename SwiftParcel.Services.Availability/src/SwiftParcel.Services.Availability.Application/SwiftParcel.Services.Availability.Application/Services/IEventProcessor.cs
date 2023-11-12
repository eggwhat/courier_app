using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Availability.Core.Events;

namespace SwiftParcel.Services.Availability.Application.Services
{
    public interface IEventProcessor
    {
         Task ProcessAsync(IEnumerable<IDomainEvent> events);
    }
}