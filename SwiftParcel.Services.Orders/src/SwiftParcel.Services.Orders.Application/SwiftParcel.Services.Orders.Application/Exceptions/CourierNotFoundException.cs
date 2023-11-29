using System;

namespace SwiftParcel.Services.Orders.Application.Exceptions
{
    public class CourierNotFoundException : AppException
    {
        public override string Code { get; } = "courier_not_found";
        public Guid Id { get; }
        
        public CourierNotFoundException(Guid id) : base($"Courier with id: {id} was not found.")
        {
            Id = id;
        }
    }
}