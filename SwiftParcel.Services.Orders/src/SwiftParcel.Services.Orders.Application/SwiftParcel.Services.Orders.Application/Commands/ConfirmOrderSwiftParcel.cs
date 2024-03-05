using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class ConfirmOrderSwiftParcel: ICommand
    {
        public Guid OrderId { get; }
        public ConfirmOrderSwiftParcel(ConfirmOrder command)
        {
            OrderId = command.OrderId;
        }
    }
}
