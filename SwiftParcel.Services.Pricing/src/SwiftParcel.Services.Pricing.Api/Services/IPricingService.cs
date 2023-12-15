using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Pricing.Api.Core.Entities;

namespace SwiftParcel.Services.Pricing.Api.Services
{
    public interface IPricingService
    {
        decimal CalculateParcelPrice(Parcel parcel, Customer customer = null);
    }
}