using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Convey;
using Convey.WebApi;
using SwiftParcel.Services.Couriers.Application;
using SwiftParcel.Services.Couriers.Infrastructure;
using Convey.WebApi.CQRS;
using Convey.Types;
using SwiftParcel.Services.Couriers.Application.Queries;
using SwiftParcel.Services.Couriers.Application.DTO;
using Convey.CQRS.Queries;
using SwiftParcel.Services.Couriers.Application.Commands;
using Convey.Logging;
using Convey.Secrets.Vault;

namespace SwiftParcel.Services.Couriers.Api
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
                        .Get<GetCourier, CourierDto>("couriers/{courierId}")
                        .Get<SearchCouriers, PagedResult<CourierDto>>("couriers")
                        .Post<AddCourier>("couriers",
                            afterDispatch: (cmd, ctx) => ctx.Response.Created($"couriers/{cmd.CourierId}"))
                        .Put<UpdateCourier>("couriers/{courierId}")
                        .Delete<DeleteCourier>("couriers/{courierId}")
                    ))
                .UseLogging()
                .UseVault()
                .Build()
                .RunAsync();
    }
}