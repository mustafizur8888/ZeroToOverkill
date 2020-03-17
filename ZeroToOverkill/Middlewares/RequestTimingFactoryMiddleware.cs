using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ZeroToOverkill.Middlewares
{
    public class RequestTimingFactoryMiddleware:IMiddleware
    {
        private readonly ILogger<RequestTimingFactoryMiddleware> _logger;

        public RequestTimingFactoryMiddleware(ILogger<RequestTimingFactoryMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var watch = Stopwatch.StartNew();
            await next(context);
            watch.Stop();;
            _logger.LogWarning("Request took {requestTime}ms", watch.ElapsedMilliseconds);

        }
    }
}
