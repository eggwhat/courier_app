using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey;
using Convey.Logging.CQRS;
using Microsoft.Extensions.DependencyInjection;
using SwiftParcel.Services.Parcels.Application.Commands;

namespace SwiftParcel.Services.Parcels.Infrastructure.Logging
{
    internal static class Extensions
    {
          public static IConveyBuilder AddHandlersLogging(this IConveyBuilder builder)
        {
            var assembly = typeof(AddParcel).Assembly;
            
            builder.Services.AddSingleton<IMessageToLogTemplateMapper>(new MessageToLogTemplateMapper());
            
            return builder
                .AddCommandHandlersLogging(assembly)
                .AddEventHandlersLogging(assembly);
        }
    }
}