using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Application.DTO;
using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class CreateOrderBaronomat: ICommand
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public ParcelDto Parcel { get; }
        public string Name { get; }
        public string Email { get; }

        public CreateOrderBaronomat(CreateOrder command, ParcelDto parcelDto)
        {
            OrderId = command.OrderId;
            CustomerId = command.CustomerId;
            Parcel = parcelDto;
            Name = command.Name;
            Email = command.Email;
        }
    }
}
