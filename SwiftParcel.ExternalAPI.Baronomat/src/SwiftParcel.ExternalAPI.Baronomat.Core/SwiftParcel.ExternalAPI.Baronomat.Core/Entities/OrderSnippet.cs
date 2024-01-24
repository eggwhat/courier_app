namespace SwiftParcel.ExternalAPI.Baronomat.Core.Entities
{
    public class OrderSnippet
    {
        public Guid CustomerId { get; set; }
        public int OrderId { get; set; }

        public OrderSnippet(Guid CustomerId, int OrderId)
        {
            this.OrderId = OrderId;
            this.CustomerId = CustomerId;
        }
    }
}