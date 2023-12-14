using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Pricing.Api.Core.Entities;

namespace SwiftParcel.Services.Pricing.Api.Core.Services
{
    public interface ICustomerDiscountsService
    {
        decimal CalculateDiscount(Customer customer);
    }
}