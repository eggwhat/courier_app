using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Application.Exceptions
{
    public class UnauthorizedParcelAccessException : AppException
    {
        public override string Code { get; } = "unauthorized_parcel_access";
        public Guid ParcelId { get; }
        public Guid CustomerId { get; }

        public UnauthorizedParcelAccessException(Guid parcelId, Guid customerId)
            : base($"Unauthorized access to parcel with id: '{parcelId}' by customer with id: '{customerId}'.")
        {
            ParcelId = parcelId;
            CustomerId = customerId;
        }
    }
}
