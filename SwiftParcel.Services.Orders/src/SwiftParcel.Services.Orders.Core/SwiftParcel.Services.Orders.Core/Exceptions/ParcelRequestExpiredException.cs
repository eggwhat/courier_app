namespace SwiftParcel.Services.Orders.Core.Exceptions
{
    public class ParcelRequestExpiredException : DomainException
    {
        public override string Code { get; } = "parcel_request_expired";
        public Guid ParcelId { get; }

        public ParcelRequestExpiredException(Guid parcelId, DateTime validTo, DateTime now) : 
        base($"Parcel request with id: '{parcelId}' has expired. Requested at: {now}, valid to: {validTo}.")
        {
            ParcelId = parcelId;
        }
    }
}