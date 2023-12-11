using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace SwiftParcel.Services.Orders.Application.Events.External
{
    [Message("customers")]
    public class CustomerCreated : IEvent
    {
        public Guid CustomerId { get; }
        public string Email { get; }
        public string FullName { get; }

        public CustomerCreated(Guid customerId, string email, string fullName)
        {
            CustomerId = customerId;
            Email = email;
            FullName = fullName;
        }
    }
}