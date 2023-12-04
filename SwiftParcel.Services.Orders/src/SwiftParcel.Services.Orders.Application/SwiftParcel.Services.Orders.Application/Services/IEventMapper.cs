using Convey.CQRS.Events;
using SwiftParcel.Services.Orders.Core;

namespace SwiftParcel.Services.Orders.Application.Services
{
    public interface IEventMapper
    {
        IEvent Map(IDomainEvent @event);
        IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events);
    }
}

