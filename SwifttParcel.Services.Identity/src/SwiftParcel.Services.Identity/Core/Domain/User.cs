using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Convey.Types;



namespace SwiftParcel.Services.Identity.Core.Domain
{
    public class User : IIdentifiable<Guid>
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string Role { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

         private static readonly Regex EmailRegex = new Regex(
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.IgnoreCase | 
            RegexOptions.Compiled | 
            RegexOptions.CultureInvariant);

        protected User()
        {
        }

        public User(Guid id, string email, string role)
        {
            if (!EmailRegex.IsMatch(email))
            {
                throw new ArgumentException(ErrorCodes.InvalidEmail,
                    $"Invalid email: '{email}'.");
            }

            if (!Domain.Role.IsValid(role))
            {
                throw new ArgumentException(ErrorCodes.InvalidRole,
                    $"Invalid role: '{role}'.");
            }

            Id = id;
            Email = email.ToLowerInvariant();
            Role = role.ToLowerInvariant();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPasswordHash(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentException(ErrorCodes.InvalidPassword, "Password can not be empty.");
            }

            PasswordHash = passwordHash;
        }

    }
}