using System.Collections.Generic;
using System.Linq;
using Convey.CQRS.Events;
using SwiftParcel.Services.Orders.Core;
using SwiftParcel.Services.Orders.Core.Entities;
using SwiftParcel.Services.Orders.Core.Events;
using SwiftParcel.Services.Orders.Application.Events;
using SwiftParcel.Services.Orders.Application.Services;

namespace SwiftParcel.Services.Orders.Infrastructure.Services
{
    public class EventMapper : IEventMapper
    {
        public IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events)
            => events.Select(Map);

        public IEvent Map(IDomainEvent @event)
        {
            switch (@event)
            {
                case OrderStateChanged e:
                    switch (e.Order.Status)
                    {
                        case OrderStatus.WaitingForDecision:
                            return new OrderCreated(e.Order.Id);
                        case OrderStatus.Approved:
                            return new OrderApproved(e.Order.Id);
                        case OrderStatus.Confirmed:
                            return new OrderConfirmed(e.Order.Id, e.Order.Parcel.Width, e.Order.Parcel.Height, e.Order.Parcel.Depth,
                                e.Order.Parcel.Weight, e.Order.Parcel.Source, e.Order.Parcel.Destination, e.Order.Parcel.Priority,
                                e.Order.Parcel.AtWeekend, e.Order.Parcel.PickupDate, e.Order.Parcel.DeliveryDate);
                        case OrderStatus.PickedUp:
                            return new OrderReceived(e.Order.Id);
                        case OrderStatus.Delivered:
                            return new OrderDelivered(e.Order.Id);
                        case OrderStatus.Cancelled:
                            return new OrderCancelled(e.Order.Id, e.Order.CancellationReason);
                        case OrderStatus.CannotDeliver:
                            return new OrderCouldNotBeDelivered(e.Order.Id, e.Order.CannotDeliverReason);
                    }

                    break;
                case ParcelAdded e:
                    return new ParcelAddedToOrder(e.Order.Id, e.Parcel.Id);
            }

            return null;
        }
    }
}
