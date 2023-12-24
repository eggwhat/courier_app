using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Customers.Core.Entities;

namespace SwiftParcel.Services.Customers.Core.Services
{
    public class VipPolicy : IVipPolicy
    {
        public void ApplyVipStatusIfEligible(Customer customer)
        {
            if (customer.IsVip)
            {
                return;
            }

            if (customer.CompletedOrders.Count() < 20)
            {
                return;
            }
            
            customer.SetVip();
        }
    }
}