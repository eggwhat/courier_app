using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.OrdersCreator.Commands
{
    public class MakeOrders: ICommand
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public Guid ParcelId { get; }

        public MakeOrder(Guid orderId, Guid customerId, Guid parcelId)
        {
            OrderId = orderId == Guid.Empty ? Guid.NewGuid() : orderId;
            CustomerId = customerId;
            ParcelId = parcelId;
        }
    }
}