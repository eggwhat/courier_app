using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Convey.CQRS.Commands;
using Convey.WebApi.CQRS;
using Convey.WebApi.Requests;
using ConveyICommand = Convey.CQRS.Commands.ICommand;

namespace SwiftParcel.Services.Identity.Application.Commands
{
    //[PublicMessage]
    public class SignUp : ConveyICommand
    {
        public Guid UserId { get; }
        public string Email { get; }
        public string Password { get; }
        public string Role { get; }

        public string GoogleToken { get; set; }
        public IEnumerable<string> Permissions { get; }

        public SignUp(Guid userId, string email, string password, string role, IEnumerable<string> permissions)
        {
            UserId = userId == Guid.Empty ? Guid.NewGuid() : userId;
            Email = email;
            Password = password;
            Role = role;
            Permissions = permissions;
        }
    }
}