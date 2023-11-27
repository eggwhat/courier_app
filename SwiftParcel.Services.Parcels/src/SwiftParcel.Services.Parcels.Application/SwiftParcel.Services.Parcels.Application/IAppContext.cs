using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Application
{
    public class IAppContext
    {
        string RequestId { get; }
        IIdentityContext Identity { get; }
    }
}