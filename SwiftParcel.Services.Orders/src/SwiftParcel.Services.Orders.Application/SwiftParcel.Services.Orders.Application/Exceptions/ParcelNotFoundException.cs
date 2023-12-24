using System;

namespace SwiftParcel.Services.Orders.Application.Exceptions
{
    public class ParcelNotFoundException : AppException
    {
        public override string Code { get; } = "parcel_not_found";
        public Guid Id { get; }
        
        public ParcelNotFoundException(Guid id) : base($"Parcel with id: {id} was not found.")
        {
            Id = id;
        }
    }
}