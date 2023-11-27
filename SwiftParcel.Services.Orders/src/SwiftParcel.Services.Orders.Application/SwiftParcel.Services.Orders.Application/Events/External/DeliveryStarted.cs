using Convey.CQRS.Events;

namespace SwiftParcel.Services.Orders.Application.Events.External
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