<<<<<<< HEAD
﻿namespace SwiftParcel.Services.Orders.Application
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Orders.Application
>>>>>>> bb4eadacae4c3d0c9391d1ebc141e92ddf13e7ff
{
    public interface IIdentityContext
    {
        Guid Id { get; }
        string Role { get; }
        bool IsAuthenticated { get; }
        bool IsAdmin { get; }
        IDictionary<string, string> Claims { get; }
    }
}