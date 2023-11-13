using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey;
using Convey.Logging;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore;
using SwiftParcel.Services.OrdersCreator.Commands;

namespace SwiftParcel.Services.OrdersCreator
{
    public class Program
    {
        public static async Task Main(string[] args)
            => await WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services
                    .AddConvey()
                    .AddWebApi()
                    .AddInfrastructure()
                    .Build())
                .Configure(app => app
                    .UseApp()
                    .UseDispatcherEndpoints(endpoints => endpoints
                        .Get("", ctx => ctx.Response.WriteAsync("Welcome to SwiftParcel couriers AI order maker Service!"))
                        .Post<MakeOrder>("orders")))
                .UseLogging()
                .Build()
                .RunAsync();
    }
}