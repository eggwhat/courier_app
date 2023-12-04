using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Deliveries.Application.Commands
{
    public class FailDelivery : ICommand
    {
        public Guid DeliveryId { get; }
        public string Reason { get; }

        public FailDelivery(Guid deliveryId, string reason)
        {
            DeliveryId = deliveryId;
            Reason = reason;
        }
    }
}