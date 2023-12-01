using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Deliveries.Application.Commands
{
    public class CompleteDelivery : ICommand
    {
        public Guid DeliveryId { get; }

        public CompleteDelivery(Guid deliveryId)
            => DeliveryId = deliveryId;
    }
}