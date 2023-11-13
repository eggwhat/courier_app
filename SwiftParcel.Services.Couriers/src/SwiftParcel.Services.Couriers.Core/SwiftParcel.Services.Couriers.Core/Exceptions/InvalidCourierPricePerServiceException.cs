using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Couriers.Core.Exceptions
{
    public class InvalidCourierPricePerServiceException : DomainException
    {
        public override string Code { get; } = "invalid_courier_price_per_service";

        public InvalidCourierPricePerServiceException(decimal pricePerService)
            : base($"Courier price per service is invalid: {pricePerService}.")
        {
        }
    }
}