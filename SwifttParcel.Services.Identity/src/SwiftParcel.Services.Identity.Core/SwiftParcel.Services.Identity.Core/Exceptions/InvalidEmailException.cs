using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Identity.Core.Exceptions
{
    public class InvalidEmailException : ExceptionBase
    {
        public override string Code => "invalid_email";

        public InvalidEmailException(string email) : base($"Invalid email: {email}.")
        {
        }
    }
}