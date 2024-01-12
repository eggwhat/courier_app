using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Convey;
using Convey.Secrets.Vault;
using Convey.Logging;
using Convey.Types;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using SwiftParcel.ExternalAPI.Lecturer.Application;
using SwiftParcel.ExternalAPI.Lecturer.Application.Commands;
using SwiftParcel.ExternalAPI.Lecturer.Application.DTO;
using SwiftParcel.ExternalAPI.Lecturer.Application.Queries;
using SwiftParcel.ExternalAPI.Lecturer.Infrastructure;
using Convey.Docs.Swagger;

namespace SwiftParcel.ExternalAPI.Lecturer.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
            => await WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services
                    .AddConvey()
                    .AddWebApi()
                    .AddApplication()
                    .AddSwaggerDocs()
                    .AddInfrastructure()
                    .Build())
                .Configure(app => app
                    .UseSwagger()
                    .UseSwaggerUI()
                    .UseInfrastructure()
                    .UseDispatcherEndpoints(endpoints => endpoints
                        .Get("", ctx => ctx.Response.WriteAsync(ctx.RequestServices.GetService<AppOptions>().Name))
                        .Get<GetParcelExpirationStatus, ExpirationStatusDto>("parcels/{parcelId}/offer")
                        .Get<GetOrderRequests, IEnumerable<OrderDto>>("orders/requests")
                        .Get<GetOrders, IEnumerable<OrderDto>>("orders")
                        .Post<AddParcel>("parcels")
                        .Post<CreateOrder>("orders")
                        .Post<ConfirmOrder>("orders/{orderId}/confirm")
                        .Post<CancelOrder>("orders/{orderId}/cancel")
                    ))
                .UseLogging()
                .UseVault()
                .Build()
                .RunAsync();
    }
}