using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Convey;
using Convey.WebApi;
using SwiftParcel.Services.Pricing.Api.Infrastructure;
using Convey.WebApi.CQRS;
using SwiftParcel.Services.Pricing.Api.Queries;
using SwiftParcel.Services.Pricing.Api.dto;
using Convey.Types;
using Convey.Logging;
using Convey.Secrets.Vault;

namespace SwiftParcel.Services.Pricing.Api
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
                    .UseInfrastructure()
                    .UseDispatcherEndpoints(endpoints => endpoints
                        .Get("", ctx => ctx.Response.WriteAsync(ctx.RequestServices.GetService<AppOptions>().Name))
                        .Get<GetOrderPricing, OrderPricingDto>("pricing")))
                .UseLogging()
                .UseVault()
                .Build()
                .RunAsync();
    }
}