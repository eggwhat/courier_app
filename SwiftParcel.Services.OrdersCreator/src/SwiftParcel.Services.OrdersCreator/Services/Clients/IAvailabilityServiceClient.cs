using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.OrdersCreator.DTO;

namespace SwiftParcel.Services.OrdersCreator.Services.Clients
{
    public interface IAvailabilityServiceClient
    {
        Task<ResourceDto> GetResourceReservationsAsync(Guid resourceId);
    }
}