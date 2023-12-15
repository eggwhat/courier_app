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
            var customerDto = await _client.GetAsync(query.CustomerId);

            if (customerDto is null)
            {
                throw new CustomerNotFoundException(query.CustomerId);
            }

            var customer = customerDto.AsEntity();
            var customerDiscount = _service.CalculateDiscount(customer);

            var parcel = query.Parcel.AsEntity(); // Corrected call
            var parcelPrice = _pricingService.CalculateParcelPrice(parcel, customer); // Corrected call

            _logger.LogInformation("Calculated pricing for customer ID: {CustomerId}, " +
                           "parcel price: {ParcelPrice} $, customer discount: {CustomerDiscount} $, " +
                           "final price: {FinalPrice} $.",
                           query.CustomerId, parcelPrice, customerDiscount, parcelPrice - customerDiscount);

            return new OrderPricingDto
            {
                Parcel = query.Parcel,
                CustomerDiscount = customerDiscount,
                OrderPrice = parcelPrice,
                OrderDiscountPrice = parcelPrice - customerDiscount
            };
        }
    }
}