using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Pricing.Api.dto;

namespace SwiftParcel.Services.Pricing.Api.Services
{
    public interface ICustomersServiceClient
    {
        Task<CustomerDto> GetAsync(Guid id);
    }
}