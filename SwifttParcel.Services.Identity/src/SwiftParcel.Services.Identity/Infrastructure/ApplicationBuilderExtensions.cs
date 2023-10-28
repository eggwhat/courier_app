using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Identity.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseInitializers(this IApplicationBuilder app)
        {
            // Your initialization code here

            return app;
        }
    }
}