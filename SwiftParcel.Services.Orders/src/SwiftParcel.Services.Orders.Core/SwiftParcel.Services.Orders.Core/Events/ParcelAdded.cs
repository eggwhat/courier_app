using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Core.Events
{
     public class ParcelAdded : IDomainEvent
    {
        public Order Order { get; }
        public Parcel Parcel { get; }

        public ParcelAdded(Order order, Parcel parcel)
        {
            Order = order;
            Parcel = parcel;
        }
    }
}


