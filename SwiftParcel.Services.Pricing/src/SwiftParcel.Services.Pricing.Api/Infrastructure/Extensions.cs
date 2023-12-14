using Convey;
using Convey.CQRS.Queries;
using Convey.Discovery.Consul;
using Convey.Docs.Swagger;
using Convey.HTTP;
using Convey.LoadBalancing.Fabio;
using Convey.Metrics.AppMetrics;
using Convey.Security;
using Convey.Tracing.Jaeger;
using Convey.WebApi;
using Convey.WebApi.Swagger;
using SwiftParcel.Services.Pricing.Api.Core.Services;
using SwiftParcel.Services.Pricing.Api.Exceptions;
using SwiftParcel.Services.Pricing.Api.Services;

namespace SwiftParcel.Services.Pricing.Api.Infrastructure
{
    public static class Extensions
    {
         public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddTransient<ICustomersServiceClient, CustomersServiceClient>();
            builder.Services.AddTransient<ICustomerDiscountsService, CustomerDiscountsService>();

            return builder
                .AddErrorHandler<ExceptionToResponseMapper>()
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddHttpClient()
                .AddConsul()
                .AddFabio()
                .AddMetrics()
                .AddJaeger()
                .AddWebApiSwaggerDocs()
                .AddSecurity();
        }
        
        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseErrorHandler()
                .UseSwaggerDocs()
                .UseJaeger()
                .UseConvey()
                .UseMetrics();

            return app;
        }
    }
}