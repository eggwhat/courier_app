using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.WebApi.Requests;
using SwiftParcel.Services.Identity.Services.Messages.Commands;

namespace SwiftParcel.Services.Identity.Services.Handlers
{
    public class SignInHandler : IRequestHandler<SignIn, string>
    {
          public SignInHandler()
        {
        }

        public async Task<string> HandleAsync(SignIn request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return "jwt";
        }
    }
}