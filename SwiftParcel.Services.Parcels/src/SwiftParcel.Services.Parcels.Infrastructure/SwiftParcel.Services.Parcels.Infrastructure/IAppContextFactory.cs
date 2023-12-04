using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Parcels.Application;

namespace SwiftParcel.Services.Parcels.Infrastructure
{
    public interface IAppContextFactory
    {
        IAppContext Create();
    }
}