using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class CreateOrderMiniCurrier: ICommand
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public Guid ParcelId { get; }
        public string Name { get; }
        public string Email { get; }
        public Address Address { get; }

        public CreateOrderMiniCurrier(CreateOrder command)
        {
            OrderId = command.OrderId;
            CustomerId = command.CustomerId;
            ParcelId = command.ParcelId;
            Name = command.Name;
            Email = command.Email;
            Address = command.Address;
        }
    }
}
