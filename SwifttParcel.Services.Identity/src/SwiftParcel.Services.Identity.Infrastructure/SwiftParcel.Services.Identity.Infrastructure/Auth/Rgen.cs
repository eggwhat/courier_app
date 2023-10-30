using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using SwiftParcel.Services.Identity.Application.Services;

namespace SwiftParcel.Services.Identity.Infrastructure.Auth
{
    public class Rgen: IRgen
    {
        private static readonly string[] SpecialChars = new[] {"/", "\\", "=", "+", "?", ":", "&"};

       
        public string Generate(int length = 50, bool removeSpecialChars = true)
        {
            var bytes = new byte[length];
            RandomNumberGenerator.Fill(bytes); // Using the recommended RandomNumberGenerator static method
            var result = Convert.ToBase64String(bytes);

            return removeSpecialChars
                ? SpecialChars.Aggregate(result, (current, chars) => current.Replace(chars, string.Empty))
                : result;
        }
    }
}