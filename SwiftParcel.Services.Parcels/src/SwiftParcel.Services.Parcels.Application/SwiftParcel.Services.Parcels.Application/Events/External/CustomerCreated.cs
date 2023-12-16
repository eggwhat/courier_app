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

        public CustomerCreated(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
