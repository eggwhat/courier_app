using Convey.CQRS.Events;

namespace SwiftParcel.Services.Orders.Application.Events.External
{
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
