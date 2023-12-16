using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Application.Exceptions
{
    public class CustomerAlreadyAddedException : AppException
    {
        public override string Code { get; } = "customer_already_added";
        public Guid Id { get; }

        public CustomerAlreadyAddedException(Guid id) : base($"Customer with id: {id} has been already added.")
        {
            Id = id;
        }
    }
}
