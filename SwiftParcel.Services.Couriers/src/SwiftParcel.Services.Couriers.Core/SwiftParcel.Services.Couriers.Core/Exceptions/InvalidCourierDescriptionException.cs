using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Couriers.Core.Exceptions
{
    public class InvalidCourierDescriptionException: DomainException
    {
        public override string Code { get; } = "invalid_vehicle_description";

        public InvalidCourierDescriptionException(string description)
            : base($"Vehicle description is invalid: {description}.")
        {
        }
    }
}