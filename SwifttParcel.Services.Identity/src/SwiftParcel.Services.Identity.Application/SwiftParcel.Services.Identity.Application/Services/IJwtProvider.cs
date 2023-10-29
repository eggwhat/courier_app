using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Identity.Application.UserDTO;

namespace SwiftParcel.Services.Identity.Application.Services
{
    public interface IJwtProvider
    {
        AuthDto Create(Guid userId, string role, string audience = null,
            IDictionary<string, IEnumerable<string>> claims = null);
    }
}