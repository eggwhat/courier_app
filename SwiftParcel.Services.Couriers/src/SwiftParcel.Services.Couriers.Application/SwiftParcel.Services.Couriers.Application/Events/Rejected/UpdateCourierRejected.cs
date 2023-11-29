using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Couriers.Application.Events.Rejected
{
    public class UpdateCourierRejected : IRejectedEvent
    {
        public Guid CourierId { get; }
        public string Reason { get; }
        public string Code { get; }

        public UpdateCourierRejected(Guid courierId, string reason, string code)
        {
            CourierId = courierId;
            Reason = reason;
            Code = code;
        }
    }
}