using System;
using Convey.MessageBrokers.RabbitMQ;
using SwiftParcel.Services.Orders.Application.Commands;
using SwiftParcel.Services.Orders.Application.Exceptions;
using SwiftParcel.Services.Orders.Application.Events.Rejected;
using SwiftParcel.Services.Orders.Application.Events.External;
using SwiftParcel.Services.Orders.Core.Exceptions;
using SwiftParcel.Services.Orders.Application.Queries;


namespace SwiftParcel.Services.Orders.Infrastructure.Exceptions
{
    public class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
            => exception switch
            {
                CannotDeleteOrderException ex => (object) new DeleteOrderRejected(ex.Id, ex.Message, ex.Code),
                ParcelRequestExpiredException ex => message switch
                {
                    CreateOrder m => new CreateOrderRejected(m.OrderId, m.CustomerId, m.ParcelId, ex.Message, ex.Code),
                    _ => null
                },

                CustomerNotFoundException ex => message switch
                {
                    CreateOrder m => new CreateOrderRejected(m.OrderId, m.CustomerId, m.ParcelId, ex.Message, ex.Code),
                    _ => null
                },
                
                InvalidBuyerNameException ex => message switch
                {
                    CreateOrder m => new CreateOrderRejected(m.OrderId, m.CustomerId, m.ParcelId, ex.Message, ex.Code),
                    _ => null
                },
                InvalidBuyerEmailException ex => message switch
                {
                    CreateOrder m => new CreateOrderRejected(m.OrderId, m.CustomerId, m.ParcelId, ex.Message, ex.Code),
                    _ => null
                },
                InvalidAddressElementException ex => message switch
                {
                    CreateOrder m => new CreateOrderRejected(m.OrderId, m.CustomerId, m.ParcelId, ex.Message, ex.Code),
                    _ => null
                },

                OrderNotFoundException ex
                => message switch
                {
                    ApproveOrderOfficeWorker m => new ApproveOrderRejected(m.OrderId, ex.Message, ex.Code),
                    CancelOrderOfficeWorker m => new CancelOrderRejected(m.OrderId, ex.Message, ex.Code),
                    CancelOrder m => new CancelOrderRejected(m.OrderId, ex.Message, ex.Code),
                    ConfirmOrder m => new ConfirmOrderRejected(m.OrderId, ex.Message, ex.Code),
                    DeleteOrder m => new DeleteOrderRejected(m.OrderId, ex.Message, ex.Code),
                    DeliveryCompleted _ => new OrderForDeliveryNotFound(ex.Id, ex.Message, ex.Code),
                    DeliveryFailed _ => new OrderForDeliveryNotFound(ex.Id, ex.Message, ex.Code),
                    DeliveryPickedUp _ => new OrderForDeliveryNotFound(ex.Id, ex.Message, ex.Code),
                    _ => null
                },

                OrderRequestExpiredException ex => message switch
                {
                    ConfirmOrder m => new ConfirmOrderRejected(m.OrderId, ex.Message, ex.Code),
                    CancelOrder m => new CancelOrderRejected(m.OrderId, ex.Message, ex.Code),
                    _ => null
                },

                ParcelNotFoundException ex
                => message switch
                {
                    CreateOrder m => new CreateOrderRejected(m.OrderId, m.CustomerId, m.ParcelId, ex.Message, ex.Code),
                    _ => null
                },
                ParcelAlreadyAddedToOrderException ex => message switch
                {
                    CreateOrder m => new CreateOrderRejected(m.OrderId, m.CustomerId, m.ParcelId, ex.Message, ex.Code),
                    _ => null
                },
                UnauthorizedOrderAccessException ex
                => message switch
                {
                    CancelOrder m => new CancelOrderRejected(m.OrderId, ex.Message, ex.Code),
                    ConfirmOrder m => new ConfirmOrderRejected(m.OrderId, ex.Message, ex.Code),
                    DeleteOrder m => new DeleteOrderRejected(m.OrderId, ex.Message, ex.Code),
                    _ => null
                },
                _ => null,
            };
    }
}