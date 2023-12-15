using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace SwiftParcel.Services.OrdersCreator.Commands.External
{
    public class AddParcelToOrder : ICommand
    {
        public Guid OrderId { get; }
        public Guid ParcelId { get; }
        public Guid? CustomerId { get; }

        public AddParcelToOrder(Guid orderId, Guid parcelId, Guid? customerId)
        {
            OrderId = orderId;
            ParcelId = parcelId;
            CustomerId = customerId;
        }
    }
}