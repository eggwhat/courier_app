<<<<<<< HEAD
﻿namespace SwiftParcel.Services.Orders.Application
{
    public interface IAppContext
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Orders.Application
{
    public class IAppContext
>>>>>>> bb4eadacae4c3d0c9391d1ebc141e92ddf13e7ff
    {
        string RequestId { get; }
        IIdentityContext Identity { get; }
    }
}