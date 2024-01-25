using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Pricing.Api.Core.Entities;
using SwiftParcel.Services.Pricing.Api.Core.Services;
using SwiftParcel.Services.Pricing.Api.dto;

namespace SwiftParcel.Services.Pricing.Api.Services
{
    public class PricingService : IPricingService
    {
        private const string Currency = "Pln";
        private const decimal BaseRate = 2.50m;
        private const decimal VolumeRate = 0.05m; // Price per cubic unit
        private const decimal WeightRate = 0.10m; // Price per weight unit
        private const decimal DimensionalWeightDivisor = 5000m; // Divisor for dimensional weight calculation
        public PricingService()
        {
        }

        public (List<PriceBreakDownItemDto>, decimal) CalculateParcelPrice(Parcel parcel, Customer customer = null)
        {
            decimal dimensionCharge = Math.Round(CalculateDimensionCharge(parcel), 2) + 1.0m;
            decimal priorityCharge = parcel.HighPriority ? 5.00m : 0m;
            decimal weekendDeliveryCharge = parcel.DeliverAtWeekend ? 3.00m : 0m;
            decimal vipPackage = parcel.VipPackage ? 7.00m : 0m;
            var priceBreakDown = new List<PriceBreakDownItemDto>()
            {
                new PriceBreakDownItemDto { Amount = BaseRate, Currency = Currency, Description = "Base price" },
                new PriceBreakDownItemDto { Amount = dimensionCharge, Currency = Currency, Description = "Dimension surcharge" },
                new PriceBreakDownItemDto { Amount = priorityCharge + weekendDeliveryCharge, 
                    Currency = Currency, Description = "Pickup and delivery time" },
            };
            if(parcel.VipPackage)
            {
                priceBreakDown.Add(new PriceBreakDownItemDto { Amount = vipPackage, Currency = Currency, Description = "Vip Package" });
            }

            return (priceBreakDown, BaseRate + dimensionCharge + priorityCharge + weekendDeliveryCharge + vipPackage);
        }

        private decimal CalculateDimensionCharge(Parcel parcel)
        {
            decimal length = (decimal)parcel.Length;
            decimal width = (decimal)parcel.Width;
            decimal height = (decimal)parcel.Height;
            decimal weight = (decimal)parcel.Weight;

            decimal volume = length * width * height;
            decimal dimensionalWeight = volume / DimensionalWeightDivisor;
            decimal weightCharge = Math.Max(weight, dimensionalWeight) * WeightRate;
            decimal volumeCharge = volume * VolumeRate;

            return weightCharge + volumeCharge;
        }
    }
}