using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Application.Exceptions
{
    public class ParcelNotFoundException : AppException
    {
        public override string Code { get; } = "parcel_not_found";
        public Guid Id { get; }

        public ParcelNotFoundException(Guid id) : base($"Parcel with id: {id} was not found.")
        {
            Id = id;
        }
    }
}
