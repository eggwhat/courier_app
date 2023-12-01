using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Customers.Application.Dto
{
    public class CustomerDetailsDto : CustomerDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }  
        public string FullName => $"{FirstName} {LastName}";
        public string Address { get; set; }
        public string SourceAddress { get; set; }
        public bool IsVip { get; set; }
        public IEnumerable<Guid> CompletedOrders { get; set; }
    }
}