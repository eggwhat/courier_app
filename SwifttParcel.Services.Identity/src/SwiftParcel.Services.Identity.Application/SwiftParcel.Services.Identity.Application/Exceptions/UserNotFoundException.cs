using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Identity.Application.Exceptions
{
    public class UserNotFoundException : AppException
    {
        public override string Code { get; } = "user_not_found";
        public string UserId { get; }
        
        public UserNotFoundException(Guid userId) : base($"User with ID: '{userId}' was not found.")
        {
            UserId = userId.ToString();
        }
        public UserNotFoundException(string userId) : base($"User with ID: '{userId}' was not found.")
        {
            UserId = userId;
        }
    }
}