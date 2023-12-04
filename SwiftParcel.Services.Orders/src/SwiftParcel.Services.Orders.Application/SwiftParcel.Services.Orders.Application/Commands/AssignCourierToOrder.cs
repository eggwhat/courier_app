using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class AssignCourierToOrder: ICommand
    {
        public Guid OrderId { get; set; }
        public Guid CourierId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public AssignCourierToOrder(Guid orderId, Guid courierID, DateTime deliveryDate)
        {
            OrderId = orderId;
            CourierId = courierID;
            DeliveryDate = deliveryDate;
        }
    }
}

