using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.Persistence.MongoDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SwiftParcel.Services.Identity.Infrastructure.Mongo.Documents;

namespace SwiftParcel.Services.Identity.Infrastructure.Mongo
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMongo(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var users = scope.ServiceProvider.GetService<IMongoRepository<UserDocument, Guid>>().Collection;
                var userBuilder = Builders<UserDocument>.IndexKeys;
                Task.Run(async () => await users.Indexes.CreateOneAsync(
                    new CreateIndexModel<UserDocument>(userBuilder.Ascending(i => i.Email),
                        new CreateIndexOptions
                        {
                            Unique = true
                        })));
            }

            return builder;
        }
    }
}