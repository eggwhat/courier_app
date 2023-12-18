namespace SwiftParcel.Services.Parcels.Core.Exceptions
{
    public class InvalidParcelDateTimeException : DomainException
    {
        public InvalidParcelDateTimeException(string element, string description)
            : base("invalid_parcel_datetime_property", $"Parcel DateTime property ({element}) is invalid: {description}.")
        {
        }
    }
}
