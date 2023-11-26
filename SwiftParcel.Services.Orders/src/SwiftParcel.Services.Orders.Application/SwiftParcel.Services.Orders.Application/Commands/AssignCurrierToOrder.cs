using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class AssignCurrierToOrder: ICommand
    {
        public Guid OrderId { get; set; }
        public Guid CurrierId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public AssignCurrierToOrder(Guid orderId, Guid currierId, DateTime deliveryDate)
        {
            OrderId = orderId;
            CurrierId = currierId;
            DeliveryDate = deliveryDate;
        }
    }
}

