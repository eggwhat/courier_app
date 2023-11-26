using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Couriers.Core.Exceptions
{
    public class InvalidCourierDescriptionException: DomainException
    {
        public override string Code { get; } = "invalid_courier_description";

        public InvalidCourierDescriptionException(string description)
            : base($"Courier description is invalid: {description}.")
        {
            
        }
    }
}