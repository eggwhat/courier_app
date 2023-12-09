using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using SwiftParcel.Services.Identity.Application;

using SwiftParcel.Services.Identity.Identity.Application.UserDTO;
using SwiftParcel.Services.Identity.Application.Commands;

using SwiftParcel.Services.Identity.Application.Events;
using SwiftParcel.Services.Identity.Core.Repositories;
using Convey.Auth;
using SwiftParcel.Services.Identity.Application.Services;
using SwiftParcel.Services.Identity.Core.Exceptions;
using SwiftParcel.Services.Identity.Core.Entities;
using Microsoft.Extensions.Logging;
using SwiftParcel.Services.Identity.Application.UserDTO;
using System.Text.RegularExpressions;
using SwiftParcel.Services.Identity.Application.Exceptions;
using Google.Apis.Auth;



namespace SwiftParcel.Services.Identity.Identity.Application.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJwtProvider _jwtProvider;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMessageBroker _messageBroker;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly ILogger<IdentityService> _logger;

        private static readonly Regex EmailRegex = new Regex(
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
        public IdentityService(IUserRepository userRepository, IPasswordService passwordService,
            IJwtProvider jwtProvider, IRefreshTokenService refreshTokenService,
            IMessageBroker messageBroker, ILogger<IdentityService> logger)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _jwtProvider = jwtProvider;
            _refreshTokenService = refreshTokenService;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task<UserDto> GetAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);

            return user is null ? null : new UserDto(user);
        }

        public async Task<AuthDto> SignInAsync(SignIn command)
        {
            if (!EmailRegex.IsMatch(command.Email))
            {
                _logger.LogError($"Invalid email: {command.Email}");
                throw new InvalidEmailException(command.Email);
            }

            var user = await _userRepository.GetAsync(command.Email);
            if (user is null || !_passwordService.IsValid(user.Password, command.Password))
            {
                _logger.LogError($"User with email: {command.Email} was not found.");
                throw new InvalidCredentialsException(command.Email);
            }

            if (!_passwordService.IsValid(user.Password, command.Password))
            {
                _logger.LogError($"Invalid password for user with id: {user.Id.Value}");
                throw new InvalidCredentialsException(command.Email);
            }

            var claims = user.Permissions.Any()
                ? new Dictionary<string, IEnumerable<string>>
                {
                    ["permissions"] = user.Permissions
                }
                : null;
            var auth = _jwtProvider.Create(user.Id, user.Role, claims: claims);
            auth.RefreshToken = await _refreshTokenService.CreateAsync(user.Id);

            _logger.LogInformation($"User with id: {user.Id} has been authenticated.");
            await _messageBroker.PublishAsync(new SignedIn(user.Id, user.Role));

            return auth;
        }

        public async Task SignUpAsync(SignUp command)
        {
            if (!EmailRegex.IsMatch(command.Email))
            {
                _logger.LogError($"Invalid email: {command.Email}");
                throw new InvalidEmailException(command.Email);
            }

            var user = await _userRepository.GetAsync(command.Email);
            if (user is {})
            {
                _logger.LogError($"Email already in use: {command.Email}");
                throw new EmailInUseException(command.Email);
            }

            var role = string.IsNullOrWhiteSpace(command.Role) ? "user" : command.Role.ToLowerInvariant();
            var password = _passwordService.Hash(command.Password);
            user = new User(command.UserId, command.Email, password, role, DateTime.UtcNow, command.Permissions);
            await _userRepository.AddAsync(user);
            
            _logger.LogInformation($"Created an account for the user with id: {user.Id}.");
            await _messageBroker.PublishAsync(new SignedUp(user.Id, user.Email, user.Role));
        }

         public async Task<AuthDto> SignInWithGoogleAsync(string googleToken)
        {
            // Example pseudocode for validating the Google token and retrieving user info
            var googleUserData = await ValidateGoogleTokenAndGetUserData(googleToken);

            var user = await _userRepository.GetAsync(googleUserData.Email);
            if (user == null)
            {
                // Optionally handle auto-registration or throw an exception
                throw new UserNotFoundException(googleUserData.GoogleId);
            }

            var auth = _jwtProvider.Create(user.Id, user.Role, claims: null); // Update as needed
            auth.RefreshToken = await _refreshTokenService.CreateAsync(user.Id);

            // Log the sign-in event
            _logger.LogInformation($"User with id: {user.Id} signed in with Google.");
            await _messageBroker.PublishAsync(new SignedIn(user.Id, user.Role));

            return auth;
        }

        public async Task<AuthDto> SignUpWithGoogleAsync(string googleToken)
        {
            var googleUserData = await ValidateGoogleTokenAndGetUserData(googleToken);

            var existingUser = await _userRepository.GetAsync(googleUserData.Email);
            if (existingUser != null)
            {
                throw new EmailInUseException(googleUserData.Email);
            }

            // Create a new user entity from Google data
            var newUser = new User(Guid.NewGuid(), googleUserData.Email, "", "user", DateTime.UtcNow, new List<string>());
            await _userRepository.AddAsync(newUser);

            var auth = _jwtProvider.Create(newUser.Id, newUser.Role, claims: null); // Update as needed
            auth.RefreshToken = await _refreshTokenService.CreateAsync(newUser.Id);

            // Log the sign-up event
            _logger.LogInformation($"User with id: {newUser.Id} signed up with Google.");
            await _messageBroker.PublishAsync(new SignedUp(newUser.Id, newUser.Email, newUser.Role));

            return auth;
        }

        private async Task<GoogleUserDto> ValidateGoogleTokenAndGetUserData(string googleToken)
        {
            GoogleJsonWebSignature.Payload payload = null;
            try
            {
                // Validate the token and get the payload
                payload = await GoogleJsonWebSignature.ValidateAsync(googleToken);
            }
            catch (InvalidJwtException ex)
            {
                _logger.LogError($"Invalid JWT token: {ex.Message}");
                throw; // Or handle the exception as you see fit
            }

            if (payload == null)
            {
                throw new InvalidOperationException("Unable to validate Google token and get payload.");
            }

            // Create and return a DTO with the user's information
            return new GoogleUserDto
            {
                Email = payload.Email,
                Name = payload.Name,
                ProfilePictureUrl = payload.Picture,
                GoogleId = payload.Subject 
                
            };
        }
    }
}