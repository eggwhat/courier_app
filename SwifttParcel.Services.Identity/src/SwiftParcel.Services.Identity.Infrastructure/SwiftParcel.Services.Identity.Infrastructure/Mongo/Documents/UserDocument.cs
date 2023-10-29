using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.Types;

namespace SwiftParcel.Services.Identity.Infrastructure.Mongo.Documents
{
    internal sealed class UserDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedAt { get; set; } 
        public IEnumerable<string>? Permissions { get; set; }
    }
}