using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Identity.Identity.Application
{
    public interface IMessageBroker
    {
         Task PublishAsync<T>(T @event) where T : class, IEvent;
    }
}