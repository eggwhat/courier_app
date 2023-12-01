using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Deliveries.Application.Commands
{
    public class AddDeliveryRegistration : ICommand
    {
        public Guid DeliveryId { get; }
        public string Description { get; }
        public DateTime DateTime { get; }

        public AddDeliveryRegistration(Guid deliveryId, string description, DateTime dateTime)
        {
            DeliveryId = deliveryId;
            Description = description;
            DateTime = dateTime;
        }
    }
}