using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ZeroToOverkill.Middlewares
{
    public class RequestTimingAdHocMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestTimingAdHocMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, ILogger<RequestTimingAdHocMiddleware> logger)
        {
            var watch = Stopwatch.StartNew();
            await _next(context);
            watch.Stop();
            logger.LogTrace("Request took {requestTime}ms", watch.ElapsedMilliseconds);

        }
    }
}
