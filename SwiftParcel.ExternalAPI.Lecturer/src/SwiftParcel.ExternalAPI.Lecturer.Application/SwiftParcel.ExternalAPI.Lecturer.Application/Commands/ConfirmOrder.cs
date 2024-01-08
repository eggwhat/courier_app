using Convey.CQRS.Commands;

namespace SwiftParcel.ExternalAPI.Lecturer.Application.Commands
{
    public class ConfirmOrder: ICommand
    {
        public Guid OrderId { get; set; }
        public ConfirmOrder(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
