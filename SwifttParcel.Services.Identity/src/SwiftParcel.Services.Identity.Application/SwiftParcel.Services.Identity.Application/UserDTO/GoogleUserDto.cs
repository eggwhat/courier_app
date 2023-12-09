using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Identity.Application.UserDTO
{
    public class GoogleUserDto
    {
         public string Email { get; set; }
        public string Name { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }

        // If you need the Google Id
        public string GoogleId { get; set; }
    }
}