using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Deliveries.Application.Commands
{
    public class AssignCourierToDelivery: ICommand
    {
        public Guid DeliveryId { get; }
        public Guid CourierId { get; }
        public AssignCourierToDelivery(Guid deliveryId, Guid courierId)
        {
            DeliveryId = deliveryId;
            CourierId = courierId;
        }
    }
}