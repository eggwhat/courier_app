using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class CreateOrderSwiftParcel: ICommand
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public Guid ParcelId { get; }
        public string Name { get; }
        public string Email { get; }
        public Address Address { get; }

        public CreateOrderSwiftParcel(CreateOrder command)
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
