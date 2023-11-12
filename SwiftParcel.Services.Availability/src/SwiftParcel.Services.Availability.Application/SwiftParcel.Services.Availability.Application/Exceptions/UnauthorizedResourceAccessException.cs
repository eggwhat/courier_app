using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Availability.Application.Exceptions
{
    public class UnauthorizedResourceAccessException : AppException
    {
        public override string Code { get; } = "unauthorized_resource_access";
        public Guid ResourceId { get; }
        public Guid CustomerId { get; }

        public UnauthorizedResourceAccessException(Guid resourceId, Guid customerId)
            : base($"Unauthorized access to resource: '{resourceId}' by customer: '{customerId}'")
            => (ResourceId, CustomerId) = (resourceId, customerId);
    }
}