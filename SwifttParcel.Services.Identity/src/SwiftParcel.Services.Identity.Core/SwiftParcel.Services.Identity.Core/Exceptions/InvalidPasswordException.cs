using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Identity.Core.Exceptions
{
    public class InvalidPasswordException : ExceptionBase
    {
        public override string Code => "invalid_password";

        public InvalidPasswordException() : base($"Invalid password.")
        {
        }
    }
}