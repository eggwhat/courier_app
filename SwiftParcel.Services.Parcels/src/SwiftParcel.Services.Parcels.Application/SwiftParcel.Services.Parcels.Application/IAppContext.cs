using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Application
{
    public interface IAppContext
    {
        string RequestId { get; }
        IIdentityContext Identity { get; }
    }
}