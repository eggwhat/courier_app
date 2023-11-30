using Convey.CQRS.Events;

namespace SwiftParcel.Services.Deliveries.Application.Events
{
    public class DeliveryStarted : IEvent
    {
        public Guid OrderId { get; }
        public DateTime DateTime { get; }

        public DeliveryStarted(Guid orderId, DateTime dateTime)
        {
            OrderId = orderId;
            DateTime = dateTime;
        }
    }
}