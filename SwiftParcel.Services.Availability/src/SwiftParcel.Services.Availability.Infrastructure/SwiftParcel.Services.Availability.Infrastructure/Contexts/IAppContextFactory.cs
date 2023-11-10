using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Availability.Infrastructure.Contexts
{
    internal interface IAppContextFactory
    {
        IAppContext Create();
    }
}