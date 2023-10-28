using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Convey.CQRS.Commands;
using Convey.WebApi.CQRS;
using Convey.WebApi.Requests;

namespace SwiftParcel.Services.Identity.Application.Commands
{
   // [PublicMessage]
    public class SignIn : ICommand, IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public SignIn(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}