using Convey.CQRS.Commands;

namespace SwiftParcel.ExternalAPI.Lecturer.Application.Commands
{
    public class CancelOrder: ICommand
    {
        public Guid OrderId { get; set; }
        public CancelOrder(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}