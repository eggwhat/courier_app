using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class SendApprovalEmail: ICommand
    {
        public Guid CustomerId { get; }
        public Guid OrderId { get; }
        public decimal TotalPrice { get; }
        public IEnumerable<Parcel> Parcels { get; }
        public SendApprovalEmail(Guid orderId, Guid customerId, decimal totalPrice, IEnumerable<Parcel> parcels)
        {
            OrderId = orderId;
            CustomerId = customerId;
            TotalPrice = totalPrice;
            Parcels = parcels;
        }
    }
}