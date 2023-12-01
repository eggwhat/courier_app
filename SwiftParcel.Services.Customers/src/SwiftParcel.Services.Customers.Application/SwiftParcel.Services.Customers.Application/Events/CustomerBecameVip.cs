using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Customers.Application.Events
{
    public class CustomerBecameVip : IEvent
    {
        public Guid CustomerId { get; }

        public CustomerBecameVip(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}