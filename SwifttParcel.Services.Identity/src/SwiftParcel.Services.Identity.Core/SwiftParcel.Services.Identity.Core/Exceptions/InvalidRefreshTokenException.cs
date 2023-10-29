using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Identity.Core.Exceptions
{
    public class InvalidRefreshTokenException : AppDomainUnloadedException
    {
        public override string Code { get; } = "invalid_refresh_token";
        
        public InvalidRefreshTokenException() : base("Invalid refresh token.")
        {
        }
    }
}