using Convey;
using Convey.Logging.CQRS;
using Microsoft.Extensions.DependencyInjection;
using SwiftParcel.Services.Orders.Application.Commands;

namespace SwiftParcel.Services.Orders.Infrastructure.Logging
{
    internal static class Extensions
    {
        public static IConveyBuilder AddHandlersLogging(this IConveyBuilder builder)
        {
            var assembly = typeof(CancelOrderOfficeWorker).Assembly;
            
            builder.Services.AddSingleton<IMessageToLogTemplateMapper>(new MessageToLogTemplateMapper());
            
            return builder
                .AddCommandHandlersLogging(assembly)
                .AddEventHandlersLogging(assembly);
        }
    }
}
