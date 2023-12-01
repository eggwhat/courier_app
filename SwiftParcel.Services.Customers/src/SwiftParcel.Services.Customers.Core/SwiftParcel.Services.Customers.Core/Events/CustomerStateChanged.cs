using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Customers.Core.Entities;

namespace SwiftParcel.Services.Customers.Core.Events
{
    public class CustomerStateChanged : IDomainEvent
    {
        public Customer Customer { get; }
        public State PreviousState { get; }

        public CustomerStateChanged(Customer customer, State previousState)
        {
            Customer = customer;
            PreviousState = previousState;
        }
    }
}