using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Core.Exceptions
{
    public class InvalidParcelWeightException : DomainException
    {
        public InvalidParcelWeightException(double weight)
            : base("invalid_parcel_weight", $"Parcel weight is invalid: {weight}."
            + $" It must be greater or equal 0.1 and less or equal 100.")
        {

        }
    }
}
