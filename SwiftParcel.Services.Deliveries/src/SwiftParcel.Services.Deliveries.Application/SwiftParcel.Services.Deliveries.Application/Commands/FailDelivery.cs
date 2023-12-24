using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Deliveries.Application.Commands
{
    public class FailDelivery : ICommand
    {
        public Guid DeliveryId { get; }
        public DateTime DeliveryAttemptDate { get; }    
        public string Reason { get; }

        public FailDelivery(Guid deliveryId, DateTime deliveryAttemptDate, string reason)
        {
            DeliveryId = deliveryId;
            DeliveryAttemptDate = deliveryAttemptDate;
            Reason = reason;
        }
    }
}