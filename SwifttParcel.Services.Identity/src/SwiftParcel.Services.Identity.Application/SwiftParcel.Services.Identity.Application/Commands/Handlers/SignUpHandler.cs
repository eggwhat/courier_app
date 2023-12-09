using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using SwiftParcel.Services.Identity.Identity.Application.Services;

namespace SwiftParcel.Services.Identity.Application.Commands.Handlers
{
    public class SignUpHandler : ICommandHandler<SignUp>
    {
        private readonly IIdentityService _identityService;

        public SignUpHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        // public Task HandleAsync(SignUp command) => _identityService.SignUpAsync(command);
        public  async Task HandleAsync(SignUp command, CancellationToken cancellationToken = default) 
        {
            if (!string.IsNullOrEmpty(command.GoogleToken))
            {
                // Handle Google sign-up
                await _identityService.SignUpWithGoogleAsync(command.GoogleToken);
            }
            else
            {
                // Handle regular sign-up
                await _identityService.SignUpAsync(command);
            }
        }
    }
}