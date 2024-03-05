namespace SwiftParcel.Services.Parcels.Core.Exceptions
{
    public class InvalidParcelPickupDateException : DomainException
    {
        public DateTime PickupDate { get; }
        public DateTime Now { get; }

        public InvalidParcelPickupDateException(DateTime pickupDate, DateTime now) : base(
            $"invalid_parcel_pickup_date", $"Invalid parcel pickup date. Pickup date: '{pickupDate}'" +
             $" must be greater than now: '{now}'.")
        {
            PickupDate = pickupDate;
            Now = now;
        }
    }
}