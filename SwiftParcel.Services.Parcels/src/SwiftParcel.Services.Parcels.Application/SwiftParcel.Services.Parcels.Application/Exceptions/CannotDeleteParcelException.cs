using SwiftParcel.Services.Parcels.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Application.Exceptions
{
    public class CannotDeleteParcelException : AppException
    {
        public override string Code { get; } = "cannot_delete_parcel";
        public Guid Id { get; }

        public CannotDeleteParcelException(Guid id) : base($"Parcel with id: '{id}' cannot be deleted.")
        {
            Id = id;
        }
    }
}
