using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SwiftParcel.Services.Orders.Application.Models;

namespace SwiftParcel.Services.Orders.Application
{
    public static class Extensions
    {
        public static IConveyBuilder AddApplication(this IConveyBuilder builder)
            => builder
                .AddCommandHandlers()
                .AddEventHandlers()
                .AddInMemoryCommandDispatcher()
                .AddInMemoryEventDispatcher()
                .AddBrevo();

        internal static IConveyBuilder AddBrevo(this IConveyBuilder builder)
        {
            // this should be replaces with some cloud key vault
            var apiKey = builder.GetOptions<BrevoOptions>("brevoApiKey");
            sib_api_v3_sdk.Client.Configuration.Default.ApiKey.Add("api-key", apiKey.ApiKey);
            return builder;
        }             
    }    
}