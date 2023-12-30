using System.Threading.Tasks;
using Convey;
using Convey.Secrets.Vault;
using Convey.Logging;
using Convey.Types;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SwiftParcel.Services.Deliveries.Application;
using SwiftParcel.Services.Deliveries.Application.Commands;
using SwiftParcel.Services.Deliveries.Application.DTO;
using SwiftParcel.Services.Deliveries.Application.Queries;
using SwiftParcel.Services.Deliveries.Infrastructure;

namespace SwiftParcel.Services.Deliveries.Api
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
                        .Get<GetDelivery, DeliveryDto>("deliveries/{deliveryId}")
                        .Get<GetDeliveries, IEnumerable<DeliveryDto>>("deliveries")
                        .Get<GetDeliveriesPending, IEnumerable<DeliveryDto>>("deliveries/pending")
                        .Post<AssignCourierToDelivery>("deliveries/{deliveryId}/courier",
                            afterDispatch: (cmd, ctx) => ctx.Response.Ok($"deliveries/{cmd.DeliveryId}"))
                        .Post<PickUpDelivery>("deliveries/{deliveryId}/pick-up")
                        .Post<FailDelivery>("deliveries/{deliveryId}/fail")
                        .Post<CompleteDelivery>("deliveries/{deliveryId}/complete")))
                .UseLogging()
                .UseVault()
                .Build()
                .RunAsync();
    }
}