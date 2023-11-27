using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Core.Events
{
    public class ParcelDeleted : IDomainEvent
    {
        public Order Order { get; }
        public Parcel Parcel { get; }

        public ParcelDeleted(Order order, Parcel parcel)
        {
            Order = order;
            Parcel = parcel;
        }
    }
}


