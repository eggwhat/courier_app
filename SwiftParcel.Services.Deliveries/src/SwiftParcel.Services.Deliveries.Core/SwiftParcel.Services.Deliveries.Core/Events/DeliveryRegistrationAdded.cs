using SwiftParcel.Services.Deliveries.Core.Entities;
using SwiftParcel.Services.Deliveries.Core.ValueObjects;

namespace SwiftParcel.Services.Deliveries.Core.Events
{
    public class DeliveryRegistrationAdded : IDomainEvent
    {
        public Delivery Delivery { get; }
        public DeliveryRegistration Registration { get; }

        public DeliveryRegistrationAdded(Delivery delivery, DeliveryRegistration registration)
        {
            Delivery = delivery;
            Registration = registration;
        }
    }
}