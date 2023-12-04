using Convey.CQRS.Events;

namespace SwiftParcel.Services.Deliveries.Application.Events
{
    public class RegistrationAddedToDelivery : IEvent
    {
        public Guid DeliveryId { get; }
        public Guid OrderId { get; }
        public string Message { get; }

        public RegistrationAddedToDelivery(Guid deliveryId, Guid orderId, string message)
        {
            DeliveryId = deliveryId;
            OrderId = orderId;
            Message = message;
        }
    }
}