namespace SwiftParcel.Services.Orders.Core.Exceptions
{
    public class CannotDeleteOrderException : DomainException
    {
        public override string Code { get; } = "cannot_delete_order";
        public Guid Id { get; }
        
        public CannotDeleteOrderException(Guid id) : base($"Cannot delete order with id: {id}.")
        {
            Id = id;
        }
    }
}

