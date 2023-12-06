using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace SwiftParcel.Services.Orders.Application.Events.External
{
    [Message("deliveries")]
    public class ParcelDeleted : IEvent
    {
        public Guid ParcelId { get; }

        public ParcelDeleted(Guid parcelId)
        {
            ParcelId = parcelId;
        }
    }
}