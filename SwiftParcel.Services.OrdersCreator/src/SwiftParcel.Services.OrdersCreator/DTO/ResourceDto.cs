using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.OrdersCreator.DTO
{
    public class ResourceDto
    {
        public Guid Id { get; set; }
        public IEnumerable<ReservationDto> Reservations { get; set; }
    }
}