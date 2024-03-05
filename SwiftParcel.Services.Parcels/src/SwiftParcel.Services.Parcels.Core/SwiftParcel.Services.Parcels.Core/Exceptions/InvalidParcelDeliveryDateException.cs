namespace SwiftParcel.Services.Parcels.Core.Exceptions
{
    public class InvalidParcelDeliveryDateException : DomainException
    {
        public DateTime DeliveryDate { get; }
        public DateTime PickupDate { get; }

        public InvalidParcelDeliveryDateException(DateTime deliveryDate, DateTime pickupDate) : base(
            $"invalid_parcel_delivery_date", $"Invalid parcel delivery date. Delivery date: '{deliveryDate}'" +
             $" must be greater than pickup date: '{pickupDate}'.")
        {
            DeliveryDate = deliveryDate;
            PickupDate = pickupDate;
        }
    }
}
