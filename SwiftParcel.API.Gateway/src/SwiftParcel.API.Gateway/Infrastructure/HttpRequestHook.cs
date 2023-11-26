using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.API.Gateway.Infrastructure
{
    internal sealed class HttpRequestHook : IHttpRequestHook
    {
        private readonly IContextBuilder _contextBuilder;

        public HttpRequestHook(IContextBuilder contextBuilder)
        {
            _contextBuilder = contextBuilder;
        }


        public Task InvokeAsync(HttpRequestMessage request, ExecutionData data)
        {
            var context = JsonConvert.SerializeObject(_contextBuilder.Build(data));
            request.Headers.TryAddWithoutValidation("Correlation-Context", context);
            
            return Task.CompletedTask;
        }
    }
}