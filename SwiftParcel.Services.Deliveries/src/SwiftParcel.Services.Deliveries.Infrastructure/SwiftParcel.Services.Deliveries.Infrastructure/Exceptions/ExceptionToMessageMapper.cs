using Convey.MessageBrokers.RabbitMQ;
using SwiftParcel.Services.Deliveries.Application.Commands;
using SwiftParcel.Services.Deliveries.Application.Events.Rejected;
using SwiftParcel.Services.Deliveries.Application.Exceptions;
using SwiftParcel.Services.Deliveries.Core.Exceptions;

namespace SwiftParcel.Services.Deliveries.Infrastructure.Exceptions
{
    public class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
            => exception switch
            {
                CannotChangeDeliveryStateException ex => message switch
                {
                    StartDelivery command => new StartDeliveryRejected(command.DeliveryId, command.OrderId, ex.Message, ex.Code),
                    CompleteDelivery command => new CompleteDeliveryRejected(command.DeliveryId, ex.Message, ex.Code),
                    FailDelivery command => new FailDeliveryRejected(command.DeliveryId, ex.Message, ex.Code),
                    AssignCourierToDelivery command => new AssignCourierToDeliveryRejected(command.DeliveryId, ex.Message, ex.Code),
                    PickUpDelivery command => new PickUpDeliveryRejected(command.DeliveryId, ex.Message, ex.Code),
                    _ => null
                },
                DeliveryAlreadyStartedException  ex => message switch
                {
                    StartDelivery command => new StartDeliveryRejected(command.DeliveryId, command.OrderId, ex.Message, ex.Code),
                    _ => null,
                },
                DeliveryHasAlreadyAssignedCourierException ex => message switch
                {
                    AssignCourierToDelivery command => new AssignCourierToDeliveryRejected(command.DeliveryId, ex.Message, ex.Code),
                    _ => null,
                },
                DeliveryNotFoundException ex => message switch
                {
                    CompleteDelivery command => new CompleteDeliveryRejected(command.DeliveryId, ex.Message, ex.Code),
                    FailDelivery command => new FailDeliveryRejected(command.DeliveryId, ex.Message, ex.Code),
                    AssignCourierToDelivery command => new AssignCourierToDeliveryRejected(command.DeliveryId, ex.Message, ex.Code),
                    PickUpDelivery command => new PickUpDeliveryRejected(command.DeliveryId, ex.Message, ex.Code),
                    _ => null
                },
                _ => null
            };
    }
}