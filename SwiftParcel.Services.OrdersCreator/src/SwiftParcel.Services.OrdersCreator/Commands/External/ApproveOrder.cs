using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace SwiftParcel.Services.OrdersCreator.Commands.External
{
    public class ApproveOrder: ICommand
    {
        public Guid OrderId { get; }

        public ApproveOrder(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}