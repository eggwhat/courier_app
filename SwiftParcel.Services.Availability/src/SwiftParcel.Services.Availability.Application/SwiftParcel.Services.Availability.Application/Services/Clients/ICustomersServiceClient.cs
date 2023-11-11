using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Availability.Application.DTO;

namespace SwiftParcel.Services.Availability.Application.Services.Clients
{
    public interface ICustomersServiceClient
    {
        Task<CustomerStateDto> GetStateAsync(Guid id);
    }
}