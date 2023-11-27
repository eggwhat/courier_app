using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Customers.Application.Exceptions
{
    public class CustomerAlreadyCreatedException : AppException
    {
        public override string Code { get; } = "customer_already_created";
        public Guid CustomerId { get; }

        public CustomerAlreadyCreatedException(Guid customerId)
            : base($"Customer with id: {customerId} was already created.")
        {
            CustomerId = customerId;
        }
    }
}