using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Customers.Application.Services
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}