using Convey;
using Convey.Logging.CQRS;
using Microsoft.Extensions.DependencyInjection;
using SwiftParcel.Services.Deliveries.Application.Commands;

namespace SwiftParcel.Services.Deliveries.Infrastructure.Logging
{
    internal static class Extensions
    {
        public static IConveyBuilder AddHandlersLogging(this IConveyBuilder builder)
        {
            var assembly = typeof(CompleteDelivery).Assembly;
            
            builder.Services.AddSingleton<IMessageToLogTemplateMapper>(new MessageToLogTemplateMapper());
            
            return builder
                .AddCommandHandlersLogging(assembly)
                .AddEventHandlersLogging(assembly);
        }
    }
}