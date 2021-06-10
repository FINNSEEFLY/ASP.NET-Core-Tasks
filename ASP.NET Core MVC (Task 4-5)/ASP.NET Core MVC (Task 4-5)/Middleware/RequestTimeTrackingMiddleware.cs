using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ASP.NET_Core_MVC__Task_4_5_.Middleware
{
    public class RequestTimeTrackingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTimeTrackingMiddleware> _logger;

        public RequestTimeTrackingMiddleware(RequestDelegate next, ILogger<RequestTimeTrackingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var startTime = DateTime.Now;

            await _next(context);

            _logger.LogInformation($"The request took {(DateTime.Now - startTime).TotalMilliseconds} milliseconds");
        }
    }
}
