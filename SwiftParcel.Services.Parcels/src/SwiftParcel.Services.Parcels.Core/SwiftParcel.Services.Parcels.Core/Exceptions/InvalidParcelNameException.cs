using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Core.Exceptions
{
    namespace SwiftParcel.Services.Parcels.Core.Exceptions
    {
        public class InvalidParcelNameException : DomainException
        {
            public InvalidParcelNameException(string name)
                : base("invalid_parcel_name", $"Parcel name is invalid: {name}.")
            {

            }
        }
    }
}
