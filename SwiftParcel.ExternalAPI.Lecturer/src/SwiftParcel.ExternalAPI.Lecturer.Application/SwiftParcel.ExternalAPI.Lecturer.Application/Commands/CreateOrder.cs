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
    }
}