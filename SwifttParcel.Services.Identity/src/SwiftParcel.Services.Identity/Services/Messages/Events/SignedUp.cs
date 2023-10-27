using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Identity.Services.Messages.Events
{
    public class SignedUp : IEvent
    {
        public Guid UserId { get; }
        public string Role { get; }

        public SignedUp(Guid userId, string role)
        {
            UserId = userId;
            Role = role;
        }
    }
}