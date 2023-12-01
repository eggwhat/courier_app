using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Customers.Application;

namespace SwiftParcel.Services.Customers.Infrastructure
{
    public interface IAppContextFactory
    {
        IAppContext Create();
    }
}