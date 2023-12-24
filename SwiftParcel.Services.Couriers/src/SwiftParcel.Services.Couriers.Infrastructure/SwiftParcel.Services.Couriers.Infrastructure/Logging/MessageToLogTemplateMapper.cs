using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.Logging.CQRS;
using SwiftParcel.Services.Couriers.Application.Commands;

namespace SwiftParcel.Services.Couriers.Infrastructure.Logging
{
    internal sealed class MessageToLogTemplateMapper : IMessageToLogTemplateMapper
    {
        private static IReadOnlyDictionary<Type, HandlerLogTemplate> MessageTemplates 
            => new Dictionary<Type, HandlerLogTemplate>
            {
                {
                    typeof(AddCourier),     
                    new HandlerLogTemplate
                    {
                        After = "Added a courier with id: {CourierId}."
                    }
                },
                {
                    typeof(DeleteCourier),     
                    new HandlerLogTemplate
                    {
                        After = "Deleted a courier with id: {CourierId}."
                    }
                },
                {
                    typeof(UpdateCourier),     
                    new HandlerLogTemplate
                    {
                        After = "Updated a courier with id: {CourierId}."
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