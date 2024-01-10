using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class CancelOrderMiniCurrier: ICommand
    {
        public Guid OrderId { get; }

        public CancelOrderMiniCurrier(CancelOrder command)
        {
            OrderId = command.OrderId;
        }
    }
}