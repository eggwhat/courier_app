using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Availability.Application;

namespace SwiftParcel.Services.Availability.Infrastructure.Contexts
{
    internal interface IAppContextFactory
    {
        IAppContext Create();
    }
}