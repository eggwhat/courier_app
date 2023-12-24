using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Customers.Application.Events
{
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