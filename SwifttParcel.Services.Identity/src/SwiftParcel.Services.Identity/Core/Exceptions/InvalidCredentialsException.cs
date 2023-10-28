using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Identity.Core.Exceptions
{
    public class InvalidCredentialsException : ExceptionBase
    {
         public override string Code => "invalid_credentials";

        public InvalidCredentialsException() : base("Invalid credentials.")
        {
        }
    }
}