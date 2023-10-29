using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey;
using Convey.Auth;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.Discovery.Consul;
using Convey.Docs.Swagger;
using Convey.MessageBrokers;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Persistence.MongoDB;
using Convey.Tracing.Jaeger;
using Convey.Tracing.Jaeger.RabbitMQ;
using Convey.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MongoDB.Driver;
using Newtonsoft.Json;
using SwiftParcel.Services.Identity.Application.Commands;
using SwiftParcel.Services.Identity.Application.Services;
using SwiftParcel.Services.Identity.Core.Entities;
using SwiftParcel.Services.Identity.Core.Repositories;
using SwiftParcel.Services.Identity.Identity.Application.Services;
using SwiftParcel.Services.Identity.Infrastructure.Auth;
using SwiftParcel.Services.Identity.Infrastructure.Contexts;
using SwiftParcel.Services.Identity.Infrastructure.Decorators;
using SwiftParcel.Services.Identity.Infrastructure.Exceptions;
using SwiftParcel.Services.Identity.Infrastructure.MessageBrokers;
using SwiftParcel.Services.Identity.Infrastructure.Mongo.Documents;
using SwiftParcel.Services.Identity.Infrastructure.Mongo.Repositories;
using SwiftParcel.Services.Identity.Infrastructure.Persistence.Mongo.Repository;
using Convey.LoadBalancing.Fabio;
using Convey.MessageBrokers.Outbox;
using Convey.MessageBrokers.Outbox.Mongo;
using Convey.Persistence.Redis;
using Convey.Metrics.AppMetrics;
using Convey.WebApi.Swagger;
using Convey.Security;
using Convey.WebApi.CQRS;
using Convey.MessageBrokers.CQRS;
using SwiftParcel.Services.Identity.Application;

namespace SwiftParcel.Services.Identity.Infrastructure.Mongo
{
    public static class Extensions
    {
       public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddSingleton<IJwtProvider, JwtProvider>();
            builder.Services.AddSingleton<IPasswordService, PasswordService>();
            builder.Services.AddSingleton<IPasswordHasher<IPasswordService>, PasswordHasher<IPasswordService>>();
            builder.Services.AddTransient<IIdentityService, IdentityService>();
            builder.Services.AddTransient<IRefreshTokenService, RefreshTokenService>();
            builder.Services.AddSingleton<IRgen, Rgen>();
            builder.Services.AddTransient<IMessageBroker, MessageBroker>();
            builder.Services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IAppContextFactory, AppContextFactory>();
            builder.Services.AddTransient(ctx => ctx.GetRequiredService<IAppContextFactory>().Create());
            builder.Services.TryDecorate(typeof(ICommandHandler<>), typeof(OutboxCommandHandlerDecorator<>));
            builder.Services.TryDecorate(typeof(IEventHandler<>), typeof(OutboxEventHandlerDecorator<>));

            return builder
                .AddErrorHandler<ExceptionToResponseMapper>()
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddJwt()
                //.AddHttpClient()
                .AddConsul()
                .AddFabio()
                .AddExceptionToMessageMapper<ExceptionToMessageMapper>()
                .AddRabbitMq(plugins: p => p.AddJaegerRabbitMqPlugin())
                .AddMessageOutbox(o => o.AddMongo())
                .AddMongo()
                .AddRedis()
                .AddMetrics()
                .AddJaeger()
                .AddMongoRepository<RefreshTokenDocument, Guid>("refreshTokens")
                .AddMongoRepository<UserDocument, Guid>("users")
                .AddWebApiSwaggerDocs()
                .AddSecurity();
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseErrorHandler()
                .UseSwaggerDocs()
                .UseJaeger()
                .UseConvey()
                .UseAccessTokenValidator()
                //.UseMongo()
                .UsePublicContracts<ContractAttribute>()
                .UseMetrics()
                .UseAuthentication()
                .UseRabbitMq()
                .SubscribeCommand<SignUp>();

            return app;
        }

        public static async Task<Guid> AuthenticateUsingJwtAsync(this HttpContext context)
        {
            var authentication = await context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);

            return authentication.Succeeded ? Guid.Parse(authentication.Principal.Identity.Name) : Guid.Empty;
        }

        internal static CorrelationContext GetCorrelationContext(this IHttpContextAccessor accessor)
            => accessor.HttpContext?.Request.Headers.TryGetValue("Correlation-Context", out var json) is true
                ? JsonConvert.DeserializeObject<CorrelationContext>(json.FirstOrDefault())
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
    }
}