using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Identity.Application.Services
{
    public interface IRgen
    {
        string Generate(int length = 50, bool removeSpecialChars = false);
    }
}