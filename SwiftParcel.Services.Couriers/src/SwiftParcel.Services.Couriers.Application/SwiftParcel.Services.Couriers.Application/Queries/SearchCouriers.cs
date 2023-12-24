using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using SwiftParcel.Services.Couriers.Application.DTO;
using SwiftParcel.Services.Couriers.Core.Entities;

namespace SwiftParcel.Services.Couriers.Application.Queries
{
    public class SearchCouriers : PagedQueryBase, IQuery<PagedResult<CourierDto>>
    {
        public double PayloadCapacity { get; set; }
        public double LoadingCapacity { get; set; }
        public Variants Variants { get; set; }
    }
}