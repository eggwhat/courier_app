using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class CancelOrder : ICommand
    {
        public Guid OrderId { get; }
        public Company Company { get; set; }

        public CancelOrder(Guid orderId, Company company)
        {
            OrderId = orderId;
            Company = company;
        }
    }
}