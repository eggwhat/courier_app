using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.Types;

namespace SwiftParcel.Services.Identity.Infrastructure.Mongo.Documents
{
    internal sealed  class RefreshTokenDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? RevokedAt { get; set; }
    }
}