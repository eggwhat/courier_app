using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using SwiftParcel.Services.Couriers.Application.DTO;

namespace SwiftParcel.Services.Couriers.Application.Queries
{
    public class GetCourier : IQuery<CourierDto>
    {
        public Guid CourierId { get; set; }
    }
}