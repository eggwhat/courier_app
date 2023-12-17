using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Pricing.Api.Core.Entities;
using SwiftParcel.Services.Pricing.Api.Core.Services;

namespace SwiftParcel.Services.Pricing.Api.Services
{
    public class PricingService : IPricingService
    {
        private readonly ICustomerDiscountsService _discountsService;

        private const decimal BaseRate = 2.50m;
        private const decimal VolumeRate = 0.05m; // Price per cubic unit
        private const decimal WeightRate = 0.10m; // Price per weight unit
        private const decimal DimensionalWeightDivisor = 5000m; // Divisor for dimensional weight calculation
        public PricingService(ICustomerDiscountsService discountsService)
        {
            _discountsService = discountsService;
        }

        public decimal CalculateParcelPrice(Parcel parcel, Customer customer = null)
        {
            decimal basePrice = CalculateBasePrice(parcel);
            decimal discount = customer != null ? _discountsService.CalculateDiscount(customer) : 0m;
            decimal priorityCharge = parcel.HighPriority ? 5.00m : 0m;
            decimal weekendDeliveryCharge = parcel.DeliverAtWeekend ? 3.00m : 0m;

            return basePrice + priorityCharge + weekendDeliveryCharge - discount;
        }

        private decimal CalculateBasePrice(Parcel parcel)
        {
            decimal length = (decimal)parcel.Length;
            decimal width = (decimal)parcel.Width;
            decimal height = (decimal)parcel.Height;
            decimal weight = (decimal)parcel.Weight;

            decimal volume = length * width * height;
            decimal dimensionalWeight = volume / DimensionalWeightDivisor;
            decimal weightCharge = Math.Max(weight, dimensionalWeight) * WeightRate;
            decimal volumeCharge = volume * VolumeRate;

            return BaseRate + weightCharge + volumeCharge;
        }
    }
}