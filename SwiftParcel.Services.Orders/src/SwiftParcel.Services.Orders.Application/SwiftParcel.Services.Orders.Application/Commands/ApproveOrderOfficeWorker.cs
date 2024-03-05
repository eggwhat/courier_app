using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class ApproveOrderOfficeWorker: ICommand
    {
        public Guid OrderId { get; }
        public ApproveOrderOfficeWorker(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}


