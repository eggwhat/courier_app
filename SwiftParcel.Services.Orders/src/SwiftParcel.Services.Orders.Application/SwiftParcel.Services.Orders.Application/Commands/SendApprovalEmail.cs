using System.Net.Sockets;
using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class SendApprovalEmail: ICommand
    {
        public Guid CustomerId { get; }
        public Guid OrderId { get; }
        public decimal TotalPrice { get; }
        public Parcel Parcel { get; }
        public SendApprovalEmail(Guid orderId, Guid customerId, decimal totalPrice, Parcel parcel)
        {
            OrderId = orderId;
            CustomerId = customerId;
            TotalPrice = totalPrice;
            Parcel = parcel;
        }
    }
}