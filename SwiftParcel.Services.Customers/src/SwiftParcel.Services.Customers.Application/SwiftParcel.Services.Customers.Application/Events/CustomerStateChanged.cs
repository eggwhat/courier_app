using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Customers.Application.Events
{
    public class CustomerStateChanged : IEvent
    {
        public Guid CustomerId { get; }
        public string CurrentState { get; }
        public string PreviousState { get; }

        public CustomerStateChanged(Guid customerId, string currentState, string previousState)
        {
            CustomerId = customerId;
            CurrentState = currentState;
            PreviousState = previousState;
        }
    }
}