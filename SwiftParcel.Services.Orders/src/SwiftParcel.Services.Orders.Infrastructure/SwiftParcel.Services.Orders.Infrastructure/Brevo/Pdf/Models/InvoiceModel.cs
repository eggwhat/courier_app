using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Infrastructure.Brevo.Pdf.Models
{
    public class InvoiceModel
    {
        public string OrderId { get; set; }
        public DateTime IssueDate { get; set; }        
        public Address SellerAddress { get; set; }
        public Address CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public Decimal TotalPrice { get; set; }
    }
}