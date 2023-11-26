using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Convey;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Convey.Types;
using Convey.CQRS.Queries;
using Convey.Logging;
using Convey.Secrets.Vault;
using SwiftParcel.Services.Orders.Infrastructure;
using SwiftParcel.Services.Orders.Application;

namespace SwiftParcel.Services.Orders.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
            => await WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services
                    .AddConvey()
                    .AddWebApi()
                    .AddApplication()
                    .AddInfrastructure()
                    .Build())
                .Configure(app => app
                    .UseInfrastructure()
                    .UseDispatcherEndpoints(endpoints => endpoints
                        .Get("", ctx => ctx.Response.WriteAsync(ctx.RequestServices.GetService<AppOptions>().Name))
                        .Get<GetOrder, OrderDto>("orders/{orderId}")
                        .Get<GetOrders, IEnumerable<OrderDto>>("orders")
                        .Delete<DeleteOrder>("orders/{orderId}")
                        .Post<CreateOrder>("orders",
                            afterDispatch: (cmd, ctx) => ctx.Response.Created($"orders/{cmd.OrderId}"))
                        .Post<AddParcelToOrder>("orders/{orderId}/parcels/{parcelId}")
                        .Delete<DeleteParcelFromOrder>("orders/{orderId}/parcels/{parcelId}")))
                .UseLogging()
                .UseVault()
                .Build()
                .RunAsync();
    }
}
