using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Pricing.Api.Exceptions
{
    public class CustomerNotFoundException : AppException
    {
        public override string Code { get; } = "customer_not_found";

        public CustomerNotFoundException(Guid id) : base($"Customer not found: {id}.")
        {
        }
    }
}