using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Customers.Core.Entities;

namespace SwiftParcel.Services.Customers.Core.Services
{
    public interface IVipPolicy
    {
        void ApplyVipStatusIfEligible(Customer customer);
    }
}