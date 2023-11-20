using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Couriers.Application.Exceptions
{
    public class CourierNotFoundException : AppException
    {
       public override string Code { get; } = "courier_not_found";
        public Guid Id { get; }

        public CourierNotFoundException(Guid id) : base($"Courier not found: {id}.")
        {
            Id = id;
        }  
    }
}