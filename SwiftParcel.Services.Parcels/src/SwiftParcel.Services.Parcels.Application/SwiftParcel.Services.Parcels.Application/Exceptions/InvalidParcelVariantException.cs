using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Application.Exceptions
{
    public class InvalidParcelVariantException : AppException
    {
        public override string Code { get; } = "invalid_parcel_variant";
        public string Variant { get; }

        public InvalidParcelVariantException(string variant) : base($"Invalid parcel variant: {variant}")
        {
            Variant = variant;
        }
    }
}
