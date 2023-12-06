using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace SwiftParcel.Services.Orders.Application.Events.External
{
    [Message("availability")]
    public class ResourceReserved : IEvent
    {
        public Guid ResourceId { get; }
        public DateTime DateTime { get; }

        public ResourceReserved(Guid resourceId, DateTime dateTime)
        {
            ResourceId = resourceId;
            DateTime = dateTime;
        }
    }
}