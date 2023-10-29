using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using SwiftParcel.Services.Identity.Identity.Application.UserDTO;

namespace SwiftParcel.Services.Identity.Application.Query
{
    public class GetUser : IQuery<UserDto>
    {
        public Guid UserId { get; set; }
    }
}