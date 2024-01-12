using Convey.MessageBrokers.RabbitMQ;
using SwiftParcel.ExternalAPI.Lecturer.Application.Exceptions;
using SwiftParcel.ExternalAPI.Lecturer.Application.Commands;
using SwiftParcel.ExternalAPI.Lecturer.Application.Events.Rejected;

namespace SwiftParcel.ExternalAPI.Lecturer.Infrastructure.Exceptions
{
    public class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
            => exception switch
            {
                InquiresServiceConnectionException ex => message switch
                {
                    AddParcel m => new AddParcelRejected(m.ParcelId, ex.Message, ex.Code),
                    _ => null
                },
                InquiresServiceException ex => message switch
                {
                    AddParcel m => new AddParcelRejected(m.ParcelId, ex.Message, ex.Code),
                    _ => null
                },
                OffersServiceConnectionException ex => message switch
                {
                    CreateOrder m => new CreateOrderRejected(m.OrderId, m.CustomerId, m.ParcelId, ex.Message, ex.Code),
                    ConfirmOrder m => new ConfirmOrderRejected(m.OrderId, ex.Message, ex.Code),
                    CancelOrder m => new CancelOrderRejected(m.OrderId, ex.Message, ex.Code),
                    _ => null
                },
                OffersServiceException ex => message switch
                {
                    CreateOrder m => new CreateOrderRejected(m.OrderId, m.CustomerId, m.ParcelId, ex.Message, ex.Code),
                    ConfirmOrder m => new ConfirmOrderRejected(m.OrderId, ex.Message, ex.Code),
                    CancelOrder m => new CancelOrderRejected(m.OrderId, ex.Message, ex.Code),
                    _ => null
                },
                OfferNotFoundException ex => message switch
                {
                    ConfirmOrder m => new ConfirmOrderRejected(m.OrderId, ex.Message, ex.Code),
                    CancelOrder m => new CancelOrderRejected(m.OrderId, ex.Message, ex.Code),
                    _ => null
                },
                _ => null
            };
    }
}