using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Identity.Core.Exceptions
{
    public class InvalidRoleException : ExceptionBase
    {
        public override string Code => "invalid_role";

        public InvalidRoleException(string role) : base($"Invalid role: {role}.")
        {
        }
    }
}