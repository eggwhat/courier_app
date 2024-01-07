using Convey.CQRS.Commands;
using SwiftParcel.ExternalAPI.Lecturer.Core.Entities;

namespace SwiftParcel.ExternalAPI.Lecturer.Application.Commands
{
    public class CreateOrder: ICommand
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public Guid ParcelId { get; }
        public string Name { get; }
        public string Email { get; }
        public Address Address { get; }

        public CreateOrder(Guid orderId, Guid customerId, Guid parcelId, string name, string email, Address address)
        {
            OrderId = orderId;
            CustomerId = customerId;
            ParcelId = parcelId;
            Name = name;
            Email = email;
            Address = address;
        }
    }
}