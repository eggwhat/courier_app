using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Identity.Application.Commands
{
    public class RevokeRefreshToken : ICommand
    {
        public string RefreshToken { get; }

        public RevokeRefreshToken(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}