using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace SwiftParcel.Services.OrdersCreator.Commands.External
{
    public class CancelOrder : ICommand
    {
        public Guid OrderId { get; }
        public string Reason { get; }

        public CancelOrder(Guid orderId, string reason)
        {
            OrderId = orderId;
            Reason = reason;
        }
    }
}