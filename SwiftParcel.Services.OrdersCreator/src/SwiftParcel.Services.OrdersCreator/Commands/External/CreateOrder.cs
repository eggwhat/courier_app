using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace SwiftParcel.Services.OrdersCreator.Commands.External
{
    public class CreateOrder: ICommand
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }

        public CreateOrder(Guid id, Guid customerId)
        {
            OrderId = id == Guid.Empty ? Guid.NewGuid() : id;
            CustomerId = customerId;
        }
    }
}