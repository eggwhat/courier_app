using SwiftParcel.Services.Parcels.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Core.Exceptions
{
    public class InvalidAggregateIdException : DomainException
    {
        public InvalidAggregateIdException()
            : base("invalid_aggregate_id", $"Invalid aggregate id.")
        {
        }
    }
}