using SwiftParcel.Services.Parcels.Core.Entities;

namespace SwiftParcel.Services.Parcels.Application.DTO
{
    public class ParcelDeliveryPricingDto
    {
        public decimal OrderPrice { get; set; } // Price before discount
        public List<PriceBreakDownItem> PriceBreakDown { get; set; }
        public decimal CustomerDiscount { get; set; } // Discount amount
        public decimal OrderDiscountPrice { get; set; } // Price after discount
        public decimal FinalPrice { get; set; }
    }
}