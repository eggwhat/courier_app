using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace SwiftParcel.Services.Orders.Application.Events.External
{
    [Message("customers")]
    public class CustomerCreated : IEvent
    {
        public Guid CustomerId { get; }

        public CustomerCreated(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}