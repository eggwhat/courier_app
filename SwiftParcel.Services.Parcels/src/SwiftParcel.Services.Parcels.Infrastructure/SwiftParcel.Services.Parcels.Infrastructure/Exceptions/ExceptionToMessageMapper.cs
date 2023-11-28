using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.MessageBrokers.RabbitMQ;
using SwiftParcel.Services.Parcels.Application.Commands;
using SwiftParcel.Services.Parcels.Application.Events.Rejected;
using SwiftParcel.Services.Parcels.Application.Exceptions;
using SwiftParcel.Services.Parcels.Core.Exceptions;

namespace SwiftParcel.Services.Parcels.Infrastructure.Exceptions
{
    public class ExceptionToMessageMapper: IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
            => exception switch
            {
                InvalidAddressElementException ex => message switch
                {
                    AddParcel command => new AddParcelRejected(command.ParcelId, ex.Message,ex.Code),
                    _ => null,
                },
                InvalidParcelDescriptionException ex => message switch
                {
                    AddParcel command => new AddParcelRejected(command.ParcelId, ex.Message,ex.Code),
                    _ => null,
                },
                InvalidParcelDimensionException ex => message switch
                {
                    AddParcel command => new AddParcelRejected(command.ParcelId, ex.Message, ex.Code),
                    _ => null,
                },
                InvalidParcelPriceException ex => message switch
                {
                    AddParcel command => new AddParcelRejected(command.ParcelId, ex.Message,ex.Code),
                    _ => null,
                },
                InvalidParcelWeightException ex => message switch
                {
                    AddParcel command => new AddParcelRejected(command.ParcelId, ex.Message, ex.Code),
                    _ => null,
                },
                ParcelNotFoundException ex => message switch
                {
                    DeleteParcel command => new DeleteParcelRejected(command.ParcelId, ex.Message, ex.Code),
                    _ => null,
                },
                _ => null,
            };
    }
}