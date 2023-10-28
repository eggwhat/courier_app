using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.Auth;
using Microsoft.AspNetCore.Identity;
using SwiftParcel.Services.Identity.Core.Repositories;
using SwiftParcel.Services.Identity.Application;
using SwiftParcel.Services.Identity.Services;
using SwiftParcel.Services.Identity.Identity.Application.UserDTO;
using SwiftParcel.Services.Identity.Application.Commands;
using SwiftParcel.Services.Identity.Core.Entities;
using SwiftParcel.Services.Identity.Application.Events;




namespace SwiftParcel.Services.Identity.Identity.Application.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMessageBroker _messageBroker;

        public IdentityService(IUserRepository userRepository, IPasswordService passwordService,
            IJwtHandler jwtHandler, IMessageBroker messageBroker)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _jwtHandler = jwtHandler;
            _messageBroker = messageBroker;
        }

        public async Task<UserValidator> GetAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);

            return user is null ? null : new UserDto(user);
        }

        public async Task<JsonWebToken> SignInAsync(SignIn command)
        {
            var user = await _userRepository.GetAsync(command.Email);
            if (user is null || !_passwordService.IsValid(user.Password, command.Password))
            {
                throw new InvalidCredentialsException();
            }

            var token = _jwtHandler.CreateToken(user.Id.ToString("N"), user.Role);
            await _messageBroker.PublishAsync(new SignedIn(user.Id, user.Role));

            return token;
        }

        public async Task SignUpAsync(SignUp command)
        {
            var user = await _userRepository.GetAsync(command.Email);
            if (!(user is null))
            {
                throw new EmailInUseException(command.Email);
            }

            var role = string.IsNullOrWhiteSpace(command.Role) ? "user" : command.Role.ToLowerInvariant();
            user = new User(command.Id, command.Email, _passwordService.Hash(command.Password), role);
            await _userRepository.AddAsync(user);
            await _messageBroker.PublishAsync(new SignedUp(user.Id, user.Role));
        }
    }
}