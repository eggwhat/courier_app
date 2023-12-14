using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Pricing.Api.Queries
{
    public abstract class GetOrderPricing : IQuery<OrderPricingDto>
    {
        public Guid CustomerId { get; set; }
        public decimal OrderPrice { get; set; }
    }
}