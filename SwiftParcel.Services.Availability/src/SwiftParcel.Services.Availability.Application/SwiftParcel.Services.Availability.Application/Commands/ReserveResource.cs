using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Availability.Application.Commands
{
    public class ReserveResource : ICommand
    {
        public Guid ResourceId { get; }
        public DateTime DateTime { get; }
        public int Priority { get; }
        public Guid CustomerId { get; }

        public ReserveResource(Guid resourceId, DateTime dateTime, int priority, Guid customerId)
            => (ResourceId, DateTime, Priority, CustomerId) = (resourceId, dateTime, priority, customerId);
    }
}