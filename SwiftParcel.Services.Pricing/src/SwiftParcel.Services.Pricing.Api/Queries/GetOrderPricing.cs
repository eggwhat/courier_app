using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using SwiftParcel.Services.Pricing.Api.dto;

namespace SwiftParcel.Services.Pricing.Api.Queries
{
    public class GetOrderPricing : IQuery<OrderPricingDto>
    {
        public Guid CustomerId { get; set; }
        public decimal OrderPrice { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string HighPriority { get; set; }
        public string DeliverAtWeekend { get; set; }
        public string VipPackage { get; set; }
    }
}