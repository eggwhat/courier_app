namespace SwiftParcel.Services.Parcels.Application.Exceptions
{
    public class PricingServiceException : AppException
    {
        public override string Code { get; } = "pricing_service_error";
        public Guid ParcelId { get; }

        public PricingServiceException(Guid parcelId) : base($"Pricing service error occured for parcel with id: {parcelId}.")
        {
            ParcelId = parcelId;
        }
    }
}
