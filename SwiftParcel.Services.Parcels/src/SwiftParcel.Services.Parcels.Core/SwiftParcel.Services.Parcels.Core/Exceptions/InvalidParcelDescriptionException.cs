using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Core.Exceptions
{
    public class InvalidParcelDescriptionException : DomainException
    {
        public InvalidParcelDescriptionException(string description)
            : base("invalid_parcel_description", $"Parcel description is invalid: {description}.")
        {

        }
    }
}
