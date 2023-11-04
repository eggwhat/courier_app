using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.MessageBrokers.RabbitMQ;
using SwiftParcel.Services.Identity.Application.Commands;
using SwiftParcel.Services.Identity.Application.Events.Rejected;
using SwiftParcel.Services.Identity.Core.Exceptions;

namespace SwiftParcel.Services.Identity.Infrastructure.Exceptions
{
    public class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
            => exception switch

            {
                EmailInUseException ex => new SignUpRejected(ex.Email, ex.Message, ex.Code),
                InvalidCredentialsException ex => new SignInRejected(ex.Email, ex.Message, ex.Code),
                InvalidEmailException ex => message switch
                {
                    SignIn command => new SignInRejected(command.Email, ex.Message, ex.Code),
                    SignUpRejected command => new SignUpRejected(command.Email, ex.Message, ex.Code),
                    _ => null
                },
                _ => null
            };
    } 
}