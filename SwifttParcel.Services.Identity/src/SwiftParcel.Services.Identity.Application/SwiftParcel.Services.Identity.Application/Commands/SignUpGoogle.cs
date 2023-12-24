using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConveyICommand = Convey.CQRS.Commands.ICommand;

namespace SwiftParcel.Services.Identity.Application.Commands
{
    public class SignUpGoogle : ConveyICommand
    {
        public Guid UserId { get; }
        public string Email { get; }
        public string Password { get; }
        public string Role { get; }
        public string GoogleToken { get; set; }
        public IEnumerable<string> Permissions { get; }

        public SignUpGoogle(Guid userId, string email, string password, string role, IEnumerable<string> permissions)
        {
            UserId = userId == Guid.Empty ? Guid.NewGuid() : userId;
            Email = email;
            Password = password;
            Role = role;
            Permissions = permissions;
        }
    }
}