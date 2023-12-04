using Convey.CQRS.Events;
using SwiftParcel.Services.Deliveries.Core.Events;

namespace SwiftParcel.Services.Deliveries.Application.Services
{
    public interface IEventMapper
    {
        IEvent Map(IDomainEvent @event);
        IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events);
    }
}