using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Customers.Core.Entities;
using SwiftParcel.Services.Customers.Core.Exceptions;

namespace SwiftParcel.Services.Customers.Core
{
    public class CannotChangeCustomerStateException : DomainException
    {
        public override string Code { get; } = "cannot_change_customer_state";
        public Guid Id { get; }
        public State State { get; }

        public CannotChangeCustomerStateException(Guid id, State state) : base(
            $"Cannot change customer: {id} state to: {state}.")
        {
            Id = id;
            State = state;
        }
    }
}