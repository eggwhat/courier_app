using Convey.MessageBrokers.RabbitMQ;
using SwiftParcel.ExternalAPI.Baronomat.Application.Exceptions;
using SwiftParcel.ExternalAPI.Baronomat.Application.Commands;
using SwiftParcel.ExternalAPI.Baronomat.Application.Events.Rejected;

namespace SwiftParcel.ExternalAPI.Baronomat.Infrastructure.Exceptions
{
    public class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
            => exception switch
            {
                PriceCalculatorServiceConnectionException ex => message switch
                {
                    AddParcel m => new AddParcelRejected(m.ParcelId, ex.Message, ex.Code),
                    _ => null
                },
                PriceCalculatorServiceException ex => message switch
                {
                    AddParcel m => new AddParcelRejected(m.ParcelId, ex.Message, ex.Code),
                    _ => null
                },
                OffersServiceConnectionException ex => message switch
                {
                    CreateOrder m => new CreateOrderRejected(m.OrderId, m.CustomerId, m.OrderId, ex.Message, ex.Code),
                    _ => null
                },
                OffersServiceException ex => message switch
                {
                    CreateOrder m => new CreateOrderRejected(m.OrderId, m.CustomerId, m.OrderId, ex.Message, ex.Code),
                    _ => null
                },
                OfferNotFoundException ex => message switch
                {
                    _ => null
                },
                _ => null
            };
    }
}