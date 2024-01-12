using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class CancelOrderSwiftParcel: ICommand
    {
        public Guid OrderId { get; }

        public CancelOrderSwiftParcel(CancelOrder command)
        {
            OrderId = command.OrderId;
        }
    }
}