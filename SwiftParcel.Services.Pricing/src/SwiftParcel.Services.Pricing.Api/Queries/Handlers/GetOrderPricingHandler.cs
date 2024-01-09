using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using SwiftParcel.Services.Pricing.Api.Core.Services;
using SwiftParcel.Services.Pricing.Api.dto;
using SwiftParcel.Services.Pricing.Api.Exceptions;
using SwiftParcel.Services.Pricing.Api.Services;

namespace SwiftParcel.Services.Pricing.Api.Queries.Handlers
{
    internal sealed class GetOrderPricingHandler: IQueryHandler<GetOrderPricing, OrderPricingDto>
    {
        private readonly ICustomersServiceClient _client;
        private readonly ICustomerDiscountsService _service;
        private readonly IPricingService _pricingService;
        private readonly ILogger<GetOrderPricingHandler> _logger;

        public GetOrderPricingHandler(ICustomersServiceClient client, ICustomerDiscountsService discountDervice, IPricingService pricingService,
            ILogger<GetOrderPricingHandler> logger)
        {
            _client = client;
            _service = discountDervice;
            _pricingService = pricingService;
            _logger = logger;
        }

        public async Task<OrderPricingDto> HandleAsync(GetOrderPricing query, CancellationToken cancellationToken)
        {
            CustomerDto customerDto = null;
            decimal customerDiscount = 0m;

            if (query.CustomerId != Guid.Empty)
            {
                customerDto = await _client.GetAsync(query.CustomerId);
                if (customerDto != null)
                {
                    var customer = customerDto.AsEntity();
                    customerDiscount = _service.CalculateDiscount(customer); // Corrected to use _service
                }
            }
            
            var parcelDto = new ParcelDto
            {
                Length = query.Length,
                Width = query.Width,
                Height = query.Height,
                Weight = query.Weight,
                HighPriority = query.HighPriority == "true",
                DeliverAtWeekend = query.DeliverAtWeekend == "true",
                VipPackage = query.VipPackage == "true"
            };
            
            var parcel = parcelDto.AsEntity(); 
            var (parcelPriceBreakDown, totalPrice) = _pricingService.CalculateParcelPrice(parcel, customer: null);

            var discountedPrice = totalPrice - customerDiscount;
            var finalPrice = discountedPrice > 0 ? discountedPrice : 0m; // Ensure final price is not negative
            parcelPriceBreakDown.Add(new PriceBreakDownItemDto 
                { Amount = customerDiscount, Currency = "Pln", Description = "Customer discount" });

            _logger.LogInformation("Calculated pricing for customer ID: {CustomerId}, " +
                        "calculated price: {TotalPrice}, with discount: {CustomerDiscount} final price: {FinalPrice}", 
                        query.CustomerId, totalPrice, customerDiscount, finalPrice);

            return new OrderPricingDto
            {
                //Parcel = parcelDto,
                CustomerDiscount = customerDiscount,
                OrderPrice = 0.0m,
                PriceBreakDown = parcelPriceBreakDown,
                OrderDiscountPrice = discountedPrice,
                FinalPrice = finalPrice
            };
        }
    }
}