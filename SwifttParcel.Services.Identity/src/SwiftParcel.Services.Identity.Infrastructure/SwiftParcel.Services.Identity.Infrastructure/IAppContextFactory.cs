using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Identity.Application;


namespace SwiftParcel.Services.Identity.Infrastructure
{
    public interface IAppContextFactory
    {
        IAppContext Create();
    }
}