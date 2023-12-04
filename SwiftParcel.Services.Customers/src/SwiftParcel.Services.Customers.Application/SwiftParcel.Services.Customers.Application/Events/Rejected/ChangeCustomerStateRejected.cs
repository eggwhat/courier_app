using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Customers.Application.Events.Rejected
{
    public class ChangeCustomerStateRejected : IRejectedEvent
    {
        public Guid CustomerId { get; }
        public string State { get; }
        public string Reason { get; }
        public string Code { get; }

        public ChangeCustomerStateRejected(Guid customerId, string state, string reason, string code)
        {
            CustomerId = customerId;
            State = state;
            Reason = reason;
            Code = code;
        }
    }
}