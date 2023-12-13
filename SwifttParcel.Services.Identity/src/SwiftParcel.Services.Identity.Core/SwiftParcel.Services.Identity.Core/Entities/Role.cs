using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Identity.Core.Entities
{
    public class Role
    {
        public const string User = "user";
        public const string Admin = "admin";
        public const string Courier = "courier";

        public static bool IsValid(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                return false;
            }

            role = role.ToLowerInvariant();

            return role == User || role == Courier ||role == Admin;
        }
    }
}