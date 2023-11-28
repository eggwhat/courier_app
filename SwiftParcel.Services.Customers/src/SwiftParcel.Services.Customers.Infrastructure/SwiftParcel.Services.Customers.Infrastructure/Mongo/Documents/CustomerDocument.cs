using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.Types;
using SwiftParcel.Services.Customers.Core.Entities;

namespace SwiftParcel.Services.Customers.Infrastructure.Mongo.Documents
{
    public class CustomerDocument: IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }  
        public string Address { get; set; }
        public string SourceAddress { get; set; }
        public bool IsVip { get; set; }
        public State State { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<Guid> CompletedOrders { get; set; }
    }
}