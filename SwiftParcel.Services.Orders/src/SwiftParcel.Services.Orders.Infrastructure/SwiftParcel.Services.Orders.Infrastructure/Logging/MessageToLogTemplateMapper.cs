using System;
using System.Collections.Generic;
using Convey.Logging.CQRS;
using SwiftParcel.Services.Orders.Application.Commands;
using SwiftParcel.Services.Orders.Application.Exceptions;
using SwiftParcel.Services.Orders.Application.Events.External;

namespace SwiftParcel.Services.Orders.Infrastructure.Logging
{
    internal sealed class MessageToLogTemplateMapper : IMessageToLogTemplateMapper
    {
        private static IReadOnlyDictionary<Type, HandlerLogTemplate> MessageTemplates 
            => new Dictionary<Type, HandlerLogTemplate>
            {
                {
                    typeof(ApproveOrderOfficeWorker),     
                    new HandlerLogTemplate
                    {
                        After = "Order with id: {OrderId} has been approved."
                    }
                },
                {
                    typeof(CancelOrderOfficeWorker),     
                    new HandlerLogTemplate
                    {
                        After = "Order with id: {OrderId} has been cancelled."
                    }
                },
                {
                    typeof(CreateOrder),     
                    new HandlerLogTemplate
                    {
                        After = "Order with id: {OrderId} has been created."
                    }
                },
                {
                    typeof(DeleteOrder),     
                    new HandlerLogTemplate
                    {
                        After = "Order with id: {OrderId} has been deleted."
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
                {
                    typeof(DeliveryCompleted),     
                    new HandlerLogTemplate
                    {
                        After = "Delivery with id: {DeliveryId} for the order with id: {OrderId} has been completed."
                    }
                },
                {
                    typeof(DeliveryFailed),     
                    new HandlerLogTemplate
                    {
                        After = "Order with id: {OrderId} has been canceled due to the failed delivery, reason: {Reason}"
                    }
                },
                {
                    typeof(DeliveryPickedUp),     
                    new HandlerLogTemplate
                    {
                        After = "Delivery for the order with id: {OrderId} has started"
                    }
                }
            };
        
        public HandlerLogTemplate Map<TMessage>(TMessage message) where TMessage : class
        {
            var key = message.GetType();
            return MessageTemplates.TryGetValue(key, out var template) ? template : null;
        }
    }
}