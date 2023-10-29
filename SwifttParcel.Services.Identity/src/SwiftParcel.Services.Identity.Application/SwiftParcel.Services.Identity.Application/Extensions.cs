using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey;
using Convey.Auth;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Persistence.MongoDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SwiftParcel.Services.Identity.Application.Services;
using SwiftParcel.Services.Identity.Core.Entities;
using SwiftParcel.Services.Identity.Core.Repositories;
using SwiftParcel.Services.Identity.Identity.Application.Services;
using SwiftParcel.Services.Identity.Infrastructure.Auth;
using SwiftParcel.Services.Identity.Infrastructure.MessageBrokers;
using SwiftParcel.Services.Identity.Infrastructure.Persistence.Mongo.Repository;



namespace SwiftParcel.Services.Identity.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructureModule(this IConveyBuilder builder)
        {
            builder.Services.AddSingleton<IIdentityService, IdentityService>();
            builder.Services.AddSingleton<IPasswordService, PasswordService>();
            builder.Services.AddTransient<IMessageBroker, MessageBrokers>();
            builder.Services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddSingleton<IPasswordHasher<IPasswordService>, PasswordHasher<IPasswordService>>();

            return builder.AddJwt()
                .AddCommandHandlers()
                .AddEventHandlers()
                .AddQueryHandlers()
                .AddRabbitMq()
                .AddMongo()
                .AddMongoRepository<RefreshToken, Guid>("refreshTokens")
                .AddMongoRepository<User, Guid>("users");
                
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            // TODO:  devide the extenstion in the mongo repositories and the genegral 
            
            app.UseInitializers().UseRebbitMq();
            return app;
        }
    }
}