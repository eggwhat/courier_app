using Convey.CQRS.Events;

namespace SwiftParcel.Services.Orders.Application.Events.External
{
    public class ParcelDeleted : IEvent
    {
        public Guid ParcelId { get; }

        public ParcelDeleted(Guid parcelId)
        {
            ParcelId = parcelId;
        }
    }
}