using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Availability.Core.Exceptions
{
    public class InvalidResourceTagsException: DomainException
    {
        public override string Code { get; } = "invalid_resource_tags";
        
        public InvalidResourceTagsException() : base("Resource tags are invalid.")
        {
            
        }
    }
}