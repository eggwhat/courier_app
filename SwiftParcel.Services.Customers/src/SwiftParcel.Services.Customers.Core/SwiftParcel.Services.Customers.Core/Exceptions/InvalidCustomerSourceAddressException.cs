using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Customers.Core.Exceptions
{
    public class InvalidCustomerSourceAddressException: DomainException
    {
        public override string Code { get; } = "invalid_customer_source_address";
        public Guid Id { get; }
        public string SourceAddress { get; }

        public InvalidCustomerSourceAddressException(Guid id, string address) : base(
            $"Customer with id: {id} has invalid address.")
        {
            Id = id;
            SourceAddress = address;
        }
    }
}