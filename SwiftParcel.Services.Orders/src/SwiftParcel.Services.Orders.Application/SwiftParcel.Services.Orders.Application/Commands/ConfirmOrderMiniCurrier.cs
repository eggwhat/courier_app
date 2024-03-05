using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class ConfirmOrderMiniCurrier: ICommand
    {
        public Guid OrderId { get; }

        public ConfirmOrderMiniCurrier(ConfirmOrder command)
        {
            OrderId = command.OrderId;
        }
    }
}
