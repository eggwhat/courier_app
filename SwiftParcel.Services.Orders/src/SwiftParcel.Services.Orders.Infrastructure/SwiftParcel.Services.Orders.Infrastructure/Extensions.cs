using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.Discovery.Consul;
using Convey.Docs.Swagger;
using Convey.HTTP;
using Convey.LoadBalancing.Fabio;
using Convey.MessageBrokers;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.Outbox;
using Convey.MessageBrokers.Outbox.Mongo;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Metrics.AppMetrics;
using Convey.Persistence.MongoDB;
using Convey.Persistence.Redis;
using Convey.Security;
using Convey.Tracing.Jaeger;
using Convey.Tracing.Jaeger.RabbitMQ;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Convey.WebApi.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SwiftParcel.Services.Orders.Application;
using SwiftParcel.Services.Orders.Application.Services;
using SwiftParcel.Services.Orders.Application.Services.Clients;
using SwiftParcel.Services.Orders.Application.Commands;
using SwiftParcel.Services.Orders.Application.Events.External;
using SwiftParcel.Services.Orders.Core.Repositories;
using SwiftParcel.Services.Orders.Infrastructure.Contexts;
using SwiftParcel.Services.Orders.Infrastructure.Decorators;
using SwiftParcel.Services.Orders.Infrastructure.Exceptions;
using SwiftParcel.Services.Orders.Infrastructure.Mongo.Documents;
using SwiftParcel.Services.Orders.Infrastructure.Mongo.Repositories;
using SwiftParcel.Services.Orders.Infrastructure.Services;
using SwiftParcel.Services.Orders.Infrastructure.Services.Clients;
using SwiftParcel.Services.Orders.Infrastructure.Logging;
using SwiftParcel.Services.Orders.Infrastructure.Brevo;
using Microsoft.Extensions.Configuration;
using Jaeger;
using QuestPDF;
using QuestPDF.Infrastructure;


namespace SwiftParcel.Services.Orders.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddSingleton<IEventMapper, EventMapper>();
            builder.Services.AddTransient<IMessageBroker, MessageBroker>();
            builder.Services.AddTransient<ICustomerRepository, CustomerMongoRepository>();
            builder.Services.AddTransient<IOrderRepository, OrderMongoRepository>();
            builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            builder.Services.AddTransient<IParcelsServiceClient, ParcelsServiceClient>();
            builder.Services.AddTransient<ILecturerApiServiceClient, LecturerApiServiceClient>();
            builder.Services.AddTransient<IBaronomatApiServiceClient, BaronomatApiServiceClient>();
            builder.Services.AddTransient<IAppContextFactory, AppContextFactory>();
            builder.Services.AddTransient(ctx => ctx.GetRequiredService<IAppContextFactory>().Create());
            builder.Services.TryDecorate(typeof(ICommandHandler<>), typeof(OutboxCommandHandlerDecorator<>));
            builder.Services.TryDecorate(typeof(IEventHandler<>), typeof(OutboxEventHandlerDecorator<>));

            return builder
                .AddErrorHandler<ExceptionToResponseMapper>()
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddHttpClient()
                .AddConsul()
                .AddFabio()
                .AddRabbitMq(plugins: p => p.AddJaegerRabbitMqPlugin())
                .AddMessageOutbox(o => o.AddMongo())
                .AddExceptionToMessageMapper<ExceptionToMessageMapper>()
                .AddMongo()
                .AddRedis()
                .AddMetrics()
                .AddJaeger()
                .AddBrevo()
                .AddHandlersLogging()
                .AddMongoRepository<CustomerDocument, Guid>("customers")
                .AddMongoRepository<OrderDocument, Guid>("orders")
                .AddWebApiSwaggerDocs()
                .AddSecurity();
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseErrorHandler()
                .UseSwaggerDocs()
                .UseJaeger()
                .UseConvey()
                .UsePublicContracts<ContractAttribute>()
                .UseMetrics()
                .UseRabbitMq()
                .SubscribeCommand<AddCustomerToOrder>()
                .SubscribeCommand<ApproveOrderOfficeWorker>()
                .SubscribeCommand<CancelOrder>()
                .SubscribeCommand<CancelOrderSwiftParcel>()
                .SubscribeCommand<CancelOrderMiniCurrier>()
                .SubscribeCommand<CancelOrderOfficeWorker>()
                .SubscribeCommand<ConfirmOrder>()
                .SubscribeCommand<ConfirmOrderSwiftParcel>()
                .SubscribeCommand<ConfirmOrderMiniCurrier>()
                .SubscribeCommand<CreateOrder>()
                .SubscribeCommand<CreateOrderSwiftParcel>()
                .SubscribeCommand<CreateOrderMiniCurrier>()
                .SubscribeCommand<CreateOrderBaronomat>()
                .SubscribeCommand<DeleteOrder>()
                .SubscribeCommand<SendApprovalEmail>()
                .SubscribeCommand<SendCancellationEmail>()
                .SubscribeEvent<CustomerCreated>()
                .SubscribeEvent<DeliveryCompleted>()
                .SubscribeEvent<DeliveryFailed>()
                .SubscribeEvent<DeliveryPickedUp>();
            return app;
        }

        internal static CorrelationContext GetCorrelationContext(this IHttpContextAccessor accessor)
            => accessor.HttpContext?.Request.Headers.TryGetValue("Correlation-Context", out var json) is true
                ? JsonConvert.DeserializeObject<CorrelationContext>(json.FirstOrDefault() ?? string.Empty)
                : null;
        
        internal static IDictionary<string, object> GetHeadersToForward(this IMessageProperties messageProperties)
        {
            const string sagaHeader = "Saga";
            if (messageProperties?.Headers is null || !messageProperties.Headers.TryGetValue(sagaHeader, out var saga))
            {
                return null;
            }

            return saga is null
                ? null
                : new Dictionary<string, object>
                {
                    [sagaHeader] = saga
                };
        }
        
        internal static string GetSpanContext(this IMessageProperties messageProperties, string header)
        {
            if (messageProperties is null)
            {
                return string.Empty;
            }

            if (messageProperties.Headers.TryGetValue(header, out var span) && span is byte[] spanBytes)
            {
                return Encoding.UTF8.GetString(spanBytes);
            }

            return string.Empty;
        }
        internal static IConveyBuilder AddBrevo(this IConveyBuilder builder)
        {
            // this should be replaced with some cloud key vault
            var apiKey = builder.GetOptions<BrevoOptions>("brevoApiKey");
            sib_api_v3_sdk.Client.Configuration.Default.ApiKey.Add("api-key", apiKey.ApiKey);
            Settings.License = LicenseType.Community;
            return builder;
        }  
    }
}