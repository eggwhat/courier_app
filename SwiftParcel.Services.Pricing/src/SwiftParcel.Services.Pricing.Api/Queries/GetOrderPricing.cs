using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using SwiftParcel.Services.Pricing.Api.dto;

namespace SwiftParcel.Services.Pricing.Api.Queries
{
    public abstract class GetOrderPricing : IQuery<OrderPricingDto>
    {
        public Guid CustomerId { get; set; }
        public decimal OrderPrice { get; set; }
        public ParcelDto Parcel { get; set; }
    }
}