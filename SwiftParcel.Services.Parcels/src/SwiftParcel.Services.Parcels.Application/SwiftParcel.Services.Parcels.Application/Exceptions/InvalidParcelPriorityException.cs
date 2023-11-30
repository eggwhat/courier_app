using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Application.Exceptions
{
    public class InvalidParcelPriorityException : AppException
    {
        public override string Code { get; } = "invalid_parcel_priority";
        public string Priority { get; }

        public InvalidParcelPriorityException(string priority) : base($"Invalid parcel priority: {priority}")
        {
            Priority = priority;
        }
    }
}
