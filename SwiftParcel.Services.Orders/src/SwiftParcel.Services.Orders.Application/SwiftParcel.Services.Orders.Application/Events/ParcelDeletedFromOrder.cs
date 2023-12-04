using System;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Orders.Application.Events
{
    public class ParcelDeletedFromOrder : IEvent
    {
        public Guid OrderId { get; }
        public Guid ParcelId { get; }
        public ParcelDeletedFromOrder(Guid orderId, Guid parcelId)
        {
            OrderId = orderId;
            ParcelId = parcelId;
        }
    }
}