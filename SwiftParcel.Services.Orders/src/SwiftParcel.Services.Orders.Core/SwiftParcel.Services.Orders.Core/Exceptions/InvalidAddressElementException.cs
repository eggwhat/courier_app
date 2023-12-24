namespace SwiftParcel.Services.Orders.Core.Exceptions
{
    public class InvalidAddressElementException : DomainException
    {
        public override string Code { get; } = "invalid_address_element";
        public string AddressElement { get; }
        public string Value { get; }

        public InvalidAddressElementException(string element, string value) : base($"Invalid address element {element}: {value}.")
        {
            AddressElement = element;
            Value = value;
        }
    }
}