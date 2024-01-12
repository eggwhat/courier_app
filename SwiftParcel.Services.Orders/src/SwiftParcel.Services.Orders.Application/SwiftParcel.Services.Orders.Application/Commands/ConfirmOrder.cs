using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class ConfirmOrder: ICommand
    {
        public Guid OrderId { get; }
        public Company Company { get; set; }
        public ConfirmOrder(Guid orderId, Company company)
        {
            OrderId = orderId;
            Company = company;
        }
    }
}