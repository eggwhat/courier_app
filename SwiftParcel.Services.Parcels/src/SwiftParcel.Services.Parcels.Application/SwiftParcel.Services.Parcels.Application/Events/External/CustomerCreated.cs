using Convey.CQRS.Events;
using Convey.MessageBrokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Application.Events.External
{
    [Message("customers")]
    public class CustomerCreated : IEvent
    {   
        public Guid CustomerId { get; set; }
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
