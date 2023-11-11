using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;



namespace src.SwiftParcel.Services.Availability.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
            => await CreateWebHostBuilder(args)
                .Build()
                .RunAsync();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
            => WebHost.CreateDefaultBuilder(args)
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
                        .Get<GetResources, IEnumerable<ResourceDto>>("resources")
                        .Get<GetResource, ResourceDto>("resources/{resourceId}")
                        .Post<AddResource>("resources",
                            afterDispatch: (cmd, ctx) => ctx.Response.Created($"resources/{cmd.ResourceId}"))
                        .Post<ReserveResource>("resources/{resourceId}/reservations/{dateTime}")
                        .Delete<ReleaseResourceReservation>("resources/{resourceId}/reservations/{dateTime}")
                        .Delete<DeleteResource>("resources/{resourceId}")))
                .UseLogging()
                .UseVault();
    }
}