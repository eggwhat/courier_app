namespace SwiftParcel.Services.Deliveries.Core.Exceptions
{
    public class DeliveryHasAlreadyAssignedCourierException : DomainException
    {
        public override string Code { get; } = "delivery_has_already_assigned_courier";
        public DeliveryHasAlreadyAssignedCourierException(Guid id, Guid courierId) 
            : base($"Delivery with id:'{id}' has already assigned courier with id:'{courierId}'.")
        {
        }
    }
}