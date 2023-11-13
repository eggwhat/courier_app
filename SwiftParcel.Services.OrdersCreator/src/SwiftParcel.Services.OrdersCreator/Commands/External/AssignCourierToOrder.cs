using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace SwiftParcel.Services.OrdersCreator.Commands.External
{
    public class AssignCourierToOrder: ICommand
    {
        public Guid OrderId { get; }
        public Guid CourierId { get; }
        public DateTime DeliveryDate { get; }

        public AssignCourierToOrder(Guid orderId, Guid courierId, DateTime deliveryDate)
        {
            OrderId = orderId;
            CourierId = courierId;
            DeliveryDate = deliveryDate;
        }
    }
}