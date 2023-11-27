using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Customers.Application.Commands
{
    public class ChangeCustomerState : ICommand
    {
        public Guid CustomerId { get; }
        public string State { get; }

        public ChangeCustomerState(Guid customerId, string state)
        {
            CustomerId = customerId;
            State = state;
        }
    }
}