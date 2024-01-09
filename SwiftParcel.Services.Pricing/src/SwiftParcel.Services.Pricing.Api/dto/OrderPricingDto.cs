using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Pricing.Api.dto
{
    public class OrderPricingDto
    {   
        //public ParcelDto Parcel { get; set; }
        public decimal OrderPrice { get; set; } // Price before discount
        public List<PriceBreakDownItemDto> PriceBreakDown { get; set; }
        public decimal CustomerDiscount { get; set; } // Discount amount
        public decimal OrderDiscountPrice { get; set; } // Price after discount
        public decimal FinalPrice { get; set; }
    }
}