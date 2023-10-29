using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Identity.Core.Entities;
using SwiftParcel.Services.Identity.Application.UserDTO;

namespace SwiftParcel.Services.Identity.Application.UserDTO
{
    public class JwtDTO
    {
        public string AccessToken { get; set; }
        public string Role { get; set; }
        public long Expires { get; set; }
    }
}