using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.Types;
using SwiftParcel.Services.Identity.Core.Entities;
using SwiftParcel.Services.Identity.Core.Exceptions;

namespace SwiftParcel.Services.Identity.Core.Entities
{
    public class RefreshToken : AggregateRoot
    {
       public AggregateId UserId { get; private set; }
        public string Token { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }
        public bool Revoked => RevokedAt.HasValue;

        protected RefreshToken()
        {
        }

        public void Revoke(DateTime revokedAt)
        {
            if (Revoked)
            {
                throw new RevokedRefreshTokenException();
            }

            RevokedAt = revokedAt;
        }
        public RefreshToken(AggregateId id, AggregateId userId, string token, DateTime createdAt,
            DateTime? revokedAt = null)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new InvalidRefreshTokenException();
            }

            Id = id;
            UserId = userId;
            Token = token;
            CreatedAt = createdAt;
            RevokedAt = revokedAt;
        }


    }
}