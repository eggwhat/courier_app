using Convey.CQRS.Commands;
using SwiftParcel.ExternalAPI.Baronomat.Application.DTO;
using SwiftParcel.ExternalAPI.Baronomat.Core.Entities;

namespace SwiftParcel.ExternalAPI.Baronomat.Application.Commands
{
    public class CreateOrder: ICommand
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public ParcelDto Parcel { get; }
        public string Name { get; }
        public string Email { get; }

        public CreateOrder(Guid orderId, Guid customerId, ParcelDto parcel, string name, string email)
        {
            OrderId = orderId;
            CustomerId = customerId;
            Parcel = parcel;
            Name = name;
            Email = email;
        }
    }
}