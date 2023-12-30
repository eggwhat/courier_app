using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Deliveries.Application.Commands
{
    public class PickUpDelivery: ICommand
    {
        public Guid DeliveryId { get; }

        public PickUpDelivery(Guid deliveryId)
            => DeliveryId = deliveryId;
    }
}