using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.Logging.CQRS;
using SwiftParcel.Services.Parcels.Application.Commands;

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
            };
        
        public HandlerLogTemplate Map<TMessage>(TMessage message) where TMessage : class
        {
            var key = message.GetType();
            return MessageTemplates.TryGetValue(key, out var template) ? template : null;
        }
    }
}