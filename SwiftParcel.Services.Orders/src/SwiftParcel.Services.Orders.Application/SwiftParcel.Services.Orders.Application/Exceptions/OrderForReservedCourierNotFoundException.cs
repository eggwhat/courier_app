using System;

namespace SwiftParcel.Services.Orders.Application.Exceptions
{
    public class OrderForReservedCourierNotFoundException : AppException
    {
        public override string Code { get; } = "order_for_reserved_courier_not_found";
        public Guid CourierId { get; }
        public DateTime Date { get; }

        public OrderForReservedCourierNotFoundException(Guid courierId, DateTime date) : base(
            $"Order for reserved courier: {courierId} for date: {date} was not found.")
        {
            CourierId = courierId;
            Date = date;
        }
    }
}