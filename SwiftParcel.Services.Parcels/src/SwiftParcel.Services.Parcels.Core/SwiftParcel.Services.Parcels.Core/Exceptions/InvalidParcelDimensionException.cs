using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Core.Exceptions
{
    public class InvalidParcelDimensionException : DomainException
    {
        public string DimensionType { get; }
        public double DimensionValue { get; }
        public InvalidParcelDimensionException(string dimensionType, double dimensionValue)
            : base("invalid_parcel_dimension", $"Parcel dimension ({dimensionType}) is invalid: {dimensionValue}."
            + $" It must be greater or equal 0.2 and less than 8.")
        {
            DimensionType = dimensionType;
            DimensionValue = dimensionValue;
        }
    }
}
