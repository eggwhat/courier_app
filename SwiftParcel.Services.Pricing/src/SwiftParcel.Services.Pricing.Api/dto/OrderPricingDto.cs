using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Pricing.Api.dto
{
    public class OrderPricingDto
    {
        public decimal OrderPrice { get; set; }
        public decimal CustomerDiscount { get; set; }
        public decimal OrderDiscountPrice { get; set; }
    }
}