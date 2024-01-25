namespace SwiftParcel.Services.Parcels.Core.Exceptions
{
    public class InvalidParcelDateTimeException : DomainException
    {
        public string Element { get; }
        public string Value { get; }
        public InvalidParcelDateTimeException(string element, string value)
            : base("invalid_parcel_datetime_property", $"Parcel DateTime property ({element}) is invalid: {value}.")
        {
            Element = element;
            Value = value;
        }
    }
}
