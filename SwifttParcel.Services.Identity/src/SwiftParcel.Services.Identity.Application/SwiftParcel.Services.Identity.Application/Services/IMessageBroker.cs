using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Identity.Application.Commands;
using SwiftParcel.Services.Identity.Application.UserDTO;
using SwiftParcel.Services.Identity.Identity.Application.UserDTO;

namespace SwiftParcel.Services.Identity.Application.Services
{
    public interface IMessageBroker
    {
        Task<UserDto> GetAsync(Guid id);
        Task<JwtDTO> SignInAsync(SignIn command);
        Task SignUpAsync(SignUp command);
    }
}