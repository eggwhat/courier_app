using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chronicle;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.Discovery.Consul;
using Convey.Docs.Swagger;
using Convey.HTTP;
using Convey.LoadBalancing.Fabio;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Metrics.AppMetrics;
using Convey.Persistence.Redis;
using Convey.Security;
using Convey.WebApi;
using Convey.WebApi.Swagger;
using SwiftParcel.Services.OrdersCreator.Events.External;
using SwiftParcel.Services.OrdersCreator.Exceptions;
using SwiftParcel.Services.OrdersCreator.Services;
using SwiftParcel.Services.OrdersCreator.Services.Clients;

namespace SwiftParcel.Services.OrdersCreator
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder
                .AddErrorHandler<ExceptionToResponseMapper>()
                .AddHttpClient()
                .AddConsul()
                .AddFabio()
                .AddCommandHandlers()
                .AddEventHandlers()
                .AddInMemoryCommandDispatcher()
                .AddInMemoryEventDispatcher()
                .AddRedis()
                .AddMetrics()
                .AddRabbitMq()
                .AddWebApiSwaggerDocs()
                .AddSecurity();

            builder.Services.AddChronicle();
            builder.Services.AddTransient<IAvailabilityServiceClient, AvailabilityServiceClient>();
            builder.Services.AddTransient<ICouriersServiceClient, CouriersServiceClient>();
            builder.Services.AddTransient<IResourceReservationsService, ResourceReservationsService>();
            
            return builder;
        }

        public static IApplicationBuilder UseApp(this IApplicationBuilder app)
        {
            app.UseErrorHandler()
                .UseSwaggerDocs()
                .UseConvey()
                .UseMetrics()
                .UseRabbitMq()
                .SubscribeEvent<OrderApproved>()
                .SubscribeEvent<OrderCreated>()
                .SubscribeEvent<ParcelAddedToOrder>()
                .SubscribeEvent<ResourceReserved>()
                .SubscribeEvent<CourierAssignedToOrder>();

            return app;
        }
    }
}
