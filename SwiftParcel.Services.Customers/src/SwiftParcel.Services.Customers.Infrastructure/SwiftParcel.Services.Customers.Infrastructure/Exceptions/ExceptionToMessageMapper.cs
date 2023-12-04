using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.MessageBrokers.RabbitMQ;
using SwiftParcel.Services.Customers.Application.Commands;
using SwiftParcel.Services.Customers.Application.Events.Rejected;
using SwiftParcel.Services.Customers.Application.Exceptions;
using SwiftParcel.Services.Customers.Core;
using SwiftParcel.Services.Customers.Core.Exceptions;

namespace SwiftParcel.Services.Customers.Infrastructure.Exceptions
{
    public class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
            => exception switch
            {
                CannotChangeCustomerStateException ex => message switch
                {
                    ChangeCustomerState _ => new ChangeCustomerStateRejected(ex.Id,
                        ex.State.ToString().ToLowerInvariant(), ex.Message, ex.Code),
                    CompleteCustomerRegistration _ => new CompleteCustomerRegistrationRejected(ex.Id, ex.Message,
                        ex.Code),
                    _ => null
                },
                CustomerAlreadyRegisteredException ex => new CompleteCustomerRegistrationRejected(ex.Id, ex.Message,
                    ex.Code),
                CustomerNotFoundException ex => new CompleteCustomerRegistrationRejected(ex.Id, ex.Message, ex.Code),
                InvalidCustomerFullNameException ex => new CompleteCustomerRegistrationRejected(ex.Id, ex.Message,
                    ex.Code),
                InvalidCustomerAddressException ex => new CompleteCustomerRegistrationRejected(ex.Id, ex.Message,
                    ex.Code),
                _ => null
            };
    }
}