using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Pricing.Api.dto
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public bool IsVip { get; set; }
        public IEnumerable<Guid> CompletedOrders { get; set; }
    }
}