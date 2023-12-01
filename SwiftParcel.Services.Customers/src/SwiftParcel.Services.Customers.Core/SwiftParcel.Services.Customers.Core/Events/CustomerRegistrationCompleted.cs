using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Customers.Core.Entities;

namespace SwiftParcel.Services.Customers.Core.Events
{
    public class CustomerRegistrationCompleted : IDomainEvent
    {
        public Customer Customer { get; }

        public CustomerRegistrationCompleted(Customer customer)
        {
            Customer = customer;
        }
    }
}