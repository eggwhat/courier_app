using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Core.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; }

        public Customer(Guid id)
        {
            Id = id;
        }
    }
}
