using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Pricing.Api.Queries.Handlers
{
    public class GetOrderPricingHandler: IQueryHandler<GetOrderPricing, OrderPricingDto>
    {
        private readonly ICustomersServiceClient _client;
        private readonly ICustomerDiscountsService _service;
        private readonly ILogger<GetOrderPricingHandler> _logger;

        public GetOrderPricingHandler(ICustomersServiceClient client, ICustomerDiscountsService service,
            ILogger<GetOrderPricingHandler> logger)
        {
            _client = client;
            _service = service;
            _logger = logger;
        }

        public async Task<OrderPricingDto> HandleAsync(GetOrderPricing query)
        {
            var customer = await _client.GetAsync(query.CustomerId);

            if (customer is null)
            {
                throw new CustomerNotFoundException(query.CustomerId);
            }

            var customerDiscount = _service.CalculateDiscount(customer.AsEntity());
            var orderDiscountPrice = query.OrderPrice - customerDiscount * query.OrderPrice;
            
            _logger.LogInformation($"Calculated the pricing for the customer with id: {query.CustomerId}, " +
                                   $"base order price: {query.OrderPrice} $, discount: {customerDiscount} $, " +
                                   $"final price: {orderDiscountPrice} $.");

            return new OrderPricingDto
            {
                CustomerDiscount = customerDiscount,
                OrderPrice = query.OrderPrice,
                OrderDiscountPrice = orderDiscountPrice > 0 ? orderDiscountPrice : query.OrderPrice
            };
        }
    }
}