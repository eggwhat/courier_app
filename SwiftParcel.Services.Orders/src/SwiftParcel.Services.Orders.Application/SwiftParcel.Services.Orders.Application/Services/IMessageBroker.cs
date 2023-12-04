using Convey.CQRS.Events;

namespace SwiftParcel.Services.Orders.Application.Services
{
    public interface IMessageBroker
    {
        Task PublishAsync(params IEvent[] events);
        Task PublishAsync(IEnumerable<IEvent> events);
    }
}

