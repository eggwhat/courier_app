using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Pricing.Api.Core.Entities;
using SwiftParcel.Services.Pricing.Api.dto;

namespace SwiftParcel.Services.Pricing.Api.Services
{
    public interface IPricingService
    {
        (List<PriceBreakDownItemDto>, decimal) CalculateParcelPrice(Parcel parcel, Customer customer = null);
    }
}