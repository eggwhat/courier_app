using System.Net.Sockets;
using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class SendApprovalEmail: ICommand
    {
        public Guid OrderId { get; }
        public DateTime IssueDate { get; }
        public string CustomerName { get; }
        public string CustomerEmail { get; }
        public Address CustomerAddress { get; }
        public Parcel Parcel { get; }
        public SendApprovalEmail(Guid orderId, DateTime issueDate, string customerName, 
            string customerEmail, Address customerAddress, Parcel parcel)
        {
            OrderId = orderId;
            IssueDate = issueDate;
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            CustomerAddress = customerAddress;
            Parcel = parcel;
        }
    }
}