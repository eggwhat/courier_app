using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace SwiftParcel.Services.OrdersCreator.Commands.External
{
    public class ReserveResource : ICommand
    {
        public Guid ResourceId { get; }
        public Guid CustomerId { get; }
        public DateTime DateTime { get; }
        public int Priority { get; }

        public ReserveResource(Guid resourceId, Guid customerId, DateTime dateTime, int priority)
        {
            ResourceId = resourceId;
            DateTime = dateTime;
            Priority = priority;
            CustomerId = customerId;
        }
    }
}