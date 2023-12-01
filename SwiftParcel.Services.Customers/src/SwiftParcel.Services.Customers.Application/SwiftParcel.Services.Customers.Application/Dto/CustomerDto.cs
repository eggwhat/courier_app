using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Customers.Application.Dto
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string State { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}