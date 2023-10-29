using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Identity.Application.Commands
{
    public class UseRefreshToken : ICommand
    {
        public string RefreshToken { get; }

        public UseRefreshToken(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}