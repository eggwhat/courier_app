using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.ExternalAPI.Lecturer.Application
{
    public interface IIdentityContext
    {
        Guid Id { get; }
        string Role { get; }
        bool IsAuthenticated { get; }
        bool IsOfficeWorker { get; }
        bool IsCourier { get; }
        IDictionary<string, string> Claims { get; }
    }
}