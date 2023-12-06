using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace SwiftParcel.Services.Orders.Application.Events.External
{
    [Message("availability")]
    public class ResourceReservationCancelled : IEvent
    {
        public Guid ResourceId { get; }
        public DateTime ReservationDate { get; }

        public ResourceReservationCancelled(Guid resourceId, DateTime reservationDate)
        {
            ResourceId = resourceId;
            ReservationDate = reservationDate;
        }
    }
}
