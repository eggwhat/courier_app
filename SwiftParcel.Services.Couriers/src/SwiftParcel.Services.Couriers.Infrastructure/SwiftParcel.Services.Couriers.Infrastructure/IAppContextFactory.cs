using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Couriers.Application;

namespace SwiftParcel.Services.Couriers.Infrastructure
{
    public interface IAppContextFactory
    {
        IAppContext Create();
    }
}