using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Core.Exceptions
{
    public class InvalidParcelPriceException : DomainException
    {
        public InvalidParcelPriceException(decimal price)
            : base("invalid_parcel_price", $"Parcel price is invalid: {price}.")
        {

        }
    }
}
