using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Infrastructure.Brevo.Pdf.Models
{
    public class InvoiceModel
    {
        public string OrderId { get; set; }
        public DateTime IssueDate { get; set; }        
        public string CustomerEmail { get; set; }
        public Parcel Parcel { get; set; }
        public decimal TotalPrice { get; set; }
    }
}