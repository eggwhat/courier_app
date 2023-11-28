using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Convey;
using Convey.WebApi;
using SwiftParcel.Services.Customers.Application;
using SwiftParcel.Services.Customers.Infrastructure;
using Convey.WebApi.CQRS;
using SwiftParcel.Services.Customers.Application.Queries;
using SwiftParcel.Services.Customers.Application.Dto;
using Convey.Types;
using SwiftParcel.Services.Customers.Application.Commands;
using Convey.Logging;
using Convey.Secrets.Vault;

namespace SwiftParcel.Services.Customers.Api
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
                        .Get<GetCustomers, IEnumerable<CustomerDto>>("customers")
                        .Get<GetCustomer, CustomerDetailsDto>("customers/{customerId}")
                        .Get<GetCustomerState, CustomerStateDto>("customers/{customerId}/state")
                        .Post<CompleteCustomerRegistration>("customers",
                            afterDispatch: (cmd, ctx) => ctx.Response.Created($"customers/{cmd.CustomerId}"))
                        .Put<ChangeCustomerState>("customers/{customerId}/state/{state}",
                            afterDispatch: (cmd, ctx) => ctx.Response.NoContent())))
                .UseLogging()
                .UseVault()
                .Build()
                .RunAsync();
    }
}