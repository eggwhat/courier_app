using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Availability.Core.Exceptions
{
    public class MissingResourceTagsException : DomainException
    {
        public override string Code { get; } = "missing_resource_tags";
        
        public MissingResourceTagsException() : base("Resource tags are missing.")
        {
            
        }
    }
}