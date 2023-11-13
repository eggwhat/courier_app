using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.OrdersCreator.DTO;

namespace SwiftParcel.Services.OrdersCreator.Services
{
    public interface IResourceReservationsService
    {
        Task<ReservationDto> GetBestAsync(Guid resourceId);
    }
}