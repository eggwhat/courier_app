using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Availability.Application.Qeries
{
    public class GetResource : IQuery<ResourceDto>
    {
        public Guid ResourceId { get; set; }
    }
}