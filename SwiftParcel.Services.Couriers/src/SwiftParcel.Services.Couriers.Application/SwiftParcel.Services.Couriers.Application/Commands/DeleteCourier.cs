using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Couriers.Application.Commands
{
    public class DeleteCourier : ICommand
    {
        public Guid CourierId { get; }
        
        public DeleteCourier(Guid id)
            => CourierId = id;
    }
}