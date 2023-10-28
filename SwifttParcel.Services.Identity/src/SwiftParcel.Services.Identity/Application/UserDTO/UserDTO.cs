using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Identity.Core.Entities;


namespace SwiftParcel.Services.Identity.Identity.Application.UserDTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        
        public UserDto(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Role = user.Role;
        }
    }
}