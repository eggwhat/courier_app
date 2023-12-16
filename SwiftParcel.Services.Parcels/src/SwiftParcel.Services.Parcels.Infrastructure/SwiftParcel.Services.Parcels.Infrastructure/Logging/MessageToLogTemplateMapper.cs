using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.Logging.CQRS;
using SwiftParcel.Services.Parcels.Application.Commands;
using SwiftParcel.Services.Parcels.Application.Events.External;
using SwiftParcel.Services.Parcels.Application.Exceptions;

namespace SwiftParcel.Services.Parcels.Infrastructure.Logging
{
    internal sealed class MessageToLogTemplateMapper : IMessageToLogTemplateMapper
    {
        private static IReadOnlyDictionary<Type, HandlerLogTemplate> MessageTemplates 
            => new Dictionary<Type, HandlerLogTemplate>
            {
                {
                    typeof(AddParcel),     
                    new HandlerLogTemplate
                    {
                        After = "Added a parcel with id: {ParcelId}."
                    }
                },
                {
                    typeof(DeleteParcel),     
                    new HandlerLogTemplate
                    {
                        After = "Deleted a parcel with id: {ParcelId}."
                    }
                },
                {
                    typeof(CustomerCreated),    
                    new HandlerLogTemplate
                    {
                        After = "Added a customer with id: {CustomerId}",
                        OnError = new Dictionary<Type, string>
                        {
                            {
                                typeof(CustomerAlreadyAddedException),
                                "Customer with id: {CustomerId} was already added."

                            }
                        }
                    }
                },
            };
        
        public HandlerLogTemplate Map<TMessage>(TMessage message) where TMessage : class
        {
            var key = message.GetType();
            return MessageTemplates.TryGetValue(key, out var template) ? template : null;
        }
    }
}