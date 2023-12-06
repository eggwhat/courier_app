using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace SwiftParcel.Services.Orders.Application.Events.External
{
    [Message("deliveries")]
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