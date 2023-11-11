using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using SwiftParcel.Services.Availability.Application.DTO;

namespace SwiftParcel.Services.Availability.Application.Qeries
{
    public class GetResource : IQuery<ResourceDto>
    {
        public Guid ResourceId { get; set; }
    }
}