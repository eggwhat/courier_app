using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.Types;

namespace SwiftParcel.Services.Identity.Core.Domain
{
    public class RefreshToken : IIdentifiable<Guid>
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Token { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }
        public bool Revoked => RevokedAt.HasValue;

        protected RefreshToken()
        {
        }

        public void Revoke()
        {
            if (Revoked)
            {
                throw new ArgumentException(ErrorCodes.RefreshTokenAlreadyRevoked,
                    $"Refresh token: '{Id}' was already revoked at '{RevokedAt}'.");
            }

            RevokedAt = DateTime.UtcNow;
        }

        public RefreshToken(User user, string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("Invalid refresh token.", 
                                            nameof(token));
            }


            Id = Guid.NewGuid();
            
            UserId = user.Id;
            CreatedAt = DateTime.UtcNow;
            Token = token;
        }

    }
}