using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Convey;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using SwiftParcel.Services.Identity.Infrastructure;
using SwiftParcel.Services.Identity.Identity.Application.Services;
using SwiftParcel.Services.Identity.Application.Commands;
using Convey.CQRS.Commands;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddConvey()
    .AddWebApi()
    .AddInfrastructureModule();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Welcome to the SwiftParcel Identity service!");
    });

    endpoints.MapPost("/sign-in", async context =>
    {
        var signInCommand = await context.Request.ReadFromJsonAsync<SignIn>();
        var commandDispatcher = context.RequestServices.GetRequiredService<ICommandDispatcher>();
        await commandDispatcher.SendAsync(signInCommand);
    });

    // Add other endpoints here
});

app.Run();


// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.Logging;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore;
// using Microsoft.AspNetCore.Authentication;
// using Convey;
// using Microsoft.AspNetCore.Routing;

// using Convey.WebApi;
// using SwiftParcel.Services.Identity.Infrastructure;
// using Convey.WebApi.CQRS;
// using SwiftParcel.Services.Identity.Identity.Application.Services;
// using SwiftParcel.Services.Identity.Application.Commands;


// namespace SwiftParcel.Services.Identity
// {
//     public class Program
//     {
//        public static async Task Main(string[] args)
//             => await WebHost.CreateDefaultBuilder(args)
//                 .ConfigureServices(services => services
//                     .AddConvey()
//                     .AddWebApi()
//                     .AddInfrastructureModule())
//                 .Configure(app => app
//                     .UseErrorHandler()
//                     .UseAuthentication()
//                     .UsePublicContracts(false)
//                     .UseInfrastructure()
//                     .UseEndpoints(endpoints => endpoints
//                         .Get("", ctx => ctx.Response.WriteAsync("Welcome to Pacco Identity Service!"))
//                         .Get("me", async ctx =>
//                         {
//                             var result = await ctx.AuthenticateAsync("Bearer");
//                             if (!result.Succeeded)
//                             {
//                                 ctx.Response.StatusCode = 401;
//                                 return;
//                             }

//                             var userId = Guid.Parse(result.Principal.Identity.Name);
//                             var user = await ctx.RequestServices.GetService<IIdentityService>().GetAsync(userId);
//                             if (user is null)
//                             {
//                                 ctx.Response.StatusCode = 404;
//                                 return;
//                             }

//                             ctx.Response.WriteJson(user);
//                         })
//                         .Post<SignIn>("sign-in", async (req, ctx) =>
//                         {
//                             var token = await ctx.RequestServices.GetService<IIdentityService>().SignInAsync(req);
//                             ctx.Response.WriteJson(token);
//                         })
//                         .Post<SignUp>("sign-up", async (req, ctx) =>
//                         {
//                             await ctx.RequestServices.GetService<IIdentityService>().SignUpAsync(req);
//                             await ctx.Response.NoContent();
//                         })
//                     ))
//                 .Build()
//                 .RunAsync();
//     }
// }
