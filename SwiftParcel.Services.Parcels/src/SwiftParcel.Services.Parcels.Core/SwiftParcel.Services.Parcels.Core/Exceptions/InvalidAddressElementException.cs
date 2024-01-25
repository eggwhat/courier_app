using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Core.Exceptions
{
    public class InvalidAddressElementException : DomainException
    {
        public string AddressElement { get; }
        public string Description { get; }
        public InvalidAddressElementException(string element, string description)
            : base("invalid_address_element", $"Address element ({element}) is invalid: {description}.")
        {
            AddressElement = element;
            Description = description;
        }
    }
}
