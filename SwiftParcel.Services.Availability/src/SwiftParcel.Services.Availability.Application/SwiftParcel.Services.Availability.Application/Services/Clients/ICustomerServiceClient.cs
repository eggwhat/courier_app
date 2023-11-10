using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Availability.Application.Services.Clients
{
    public interface ICustomerServiceClient
    {
        Task<CustomerStateDto> GetStateAsync(Guid id);
    }
}