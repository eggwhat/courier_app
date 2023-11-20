using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.MessageBrokers.RabbitMQ;
using SwiftParcel.Services.Couriers.Application.Commands;
using SwiftParcel.Services.Couriers.Application.Events.Rejected;
using SwiftParcel.Services.Couriers.Application.Exceptions;
using SwiftParcel.Services.Couriers.Core.Exceptions;

namespace SwiftParcel.Services.Couriers.Infrastructure.Exeptions
{
    public class ExceptionToMessageMapper: IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
            => exception switch
            {
                InvalidCourierCapacity ex => message switch
                {
                    AddCourier command => new AddCourierRejected(command.CourierId, ex.Message,ex.Code),
                    UpdateCourier command => new UpdateCourierRejected(command.CourierId, ex.Message, ex.Code),
                    _ => null,
                },
                InvalidCourierDescriptionException ex => message switch
                {
                    AddCourier command => new AddCourierRejected(command.CourierId, ex.Message,ex.Code),
                    UpdateCourier command => new UpdateCourierRejected(command.CourierId, ex.Message, ex.Code),
                    _ => null,
                },
                InvalidCourierPricePerServiceException ex => message switch
                {
                    AddCourier command => new AddCourierRejected(command.CourierId, ex.Message,ex.Code),
                    UpdateCourier command => new UpdateCourierRejected(command.CourierId, ex.Message, ex.Code),
                    _ => null,
                },
                CourierNotFoundException ex => message switch
                {
                    UpdateCourier command => new UpdateCourierRejected(command.CourierId, ex.Message,ex.Code),
                    DeleteCourier command => new DeleteCourierRejected(command.CourierId, ex.Message, ex.Code),
                    _ => null,
                },
                _ => null,
            };
    }
}