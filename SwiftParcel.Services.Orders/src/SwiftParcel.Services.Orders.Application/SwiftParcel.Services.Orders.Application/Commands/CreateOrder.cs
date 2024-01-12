using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Application.Services;
using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class CreateOrder: ICommand
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public Guid ParcelId { get; }
        public string Name { get; }
        public string Email { get; }
        public Address Address { get; }
        public Company Company { get; }
        public CreateOrder(Guid orderId, Guid customerId, Guid parcelId, string name, string email, 
            Address address, Company company)
        {
            OrderId = orderId == Guid.Empty ? Guid.NewGuid() : orderId;
            CustomerId = customerId;
            ParcelId = parcelId;
            Name = name;
            Email = email;
            Address = address;
            Company = company;
        }
    }
}


