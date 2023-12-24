using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Deliveries.Application.Commands
{
    public class CompleteDelivery : ICommand
    {
        public Guid DeliveryId { get; }
        public DateTime DeliveryAttemptDate { get; }

        public CompleteDelivery(Guid deliveryId, DateTime deliveryAttemptDate)
        {
            DeliveryId = deliveryId;
            DeliveryAttemptDate = deliveryAttemptDate;
        }
    }
}