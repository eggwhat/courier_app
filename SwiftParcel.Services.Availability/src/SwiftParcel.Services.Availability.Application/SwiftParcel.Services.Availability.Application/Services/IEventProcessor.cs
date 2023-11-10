using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Availability.Application.Services
{
    public interface IEventProcessor
    {
         Task ProcessAsync(IEnumerable<IDomainEvent> events);
    }
}