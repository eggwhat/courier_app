using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.OrdersCreator.Events.External
{
    public class ParcelAddedToOrder : IEvent
    {
        public Guid OrderId { get; }
        public Guid ParcelId { get; }

        public ParcelAddedToOrder(Guid orderId, Guid parcelId)
        {
            OrderId = orderId;
            ParcelId = parcelId;
        }
    }
}