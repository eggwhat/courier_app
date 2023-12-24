using Convey.Logging.CQRS;
using SwiftParcel.Services.Deliveries.Application.Commands;

namespace SwiftParcel.Services.Deliveries.Infrastructure.Logging
{
    internal sealed class MessageToLogTemplateMapper : IMessageToLogTemplateMapper
    {
        private static IReadOnlyDictionary<Type, HandlerLogTemplate> MessageTemplates 
            => new Dictionary<Type, HandlerLogTemplate>
            {
                {
                    typeof(CompleteDelivery),  new HandlerLogTemplate
                    {
                        After = "Completed the delivery with id: {DeliveryId}."
                    }
                },
                {
                    typeof(FailDelivery), new HandlerLogTemplate
                    {
                        After = "Failed the delivery with id: {DeliveryId}, reason: {Reason}"
                    }
                },
                {
                    typeof(StartDelivery), new HandlerLogTemplate
                    {
                        After = "Started the delivery with id: {DeliveryId}."
                    }
                },
                {
                    typeof(PickUpDelivery), new HandlerLogTemplate
                    {
                        After = "Picked up the delivery with id: {DeliveryId}."
                    }
                },
                {
                    typeof(AssignCourierToDelivery), new HandlerLogTemplate
                    {
                        After = "Assigned courier with id: {CourierId} to the delivery with id: {DeliveryId}."
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