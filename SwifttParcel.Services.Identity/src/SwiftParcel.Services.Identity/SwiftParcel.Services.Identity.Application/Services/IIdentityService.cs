using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.Auth;
using SwiftParcel.Services.Identity.Application.Commands;
using SwiftParcel.Services.Identity.Identity.Application.UserDTO;


namespace SwiftParcel.Services.Identity.Identity.Application.Services
{
    public interface IIdentityService
    {
        Task<UserDto> GetAsync(Guid id);
        Task<JsonWebToken> SignInAsync(SignIn command);
        Task SignUpAsync(SignUp command);
    }
}