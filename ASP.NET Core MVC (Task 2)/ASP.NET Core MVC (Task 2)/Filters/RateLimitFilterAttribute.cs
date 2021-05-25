using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP.NET_Core_MVC__Task_2_.Filters
{
    public class RateLimitFilterAttribute : ActionFilterAttribute
    {
        public int RateLimit { get; set; } = 3;
        private int _currentNumOfConnections;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (_currentNumOfConnections < RateLimit)
            {
                _currentNumOfConnections++;
            }
            else
            {
                context.Result = new StatusCodeResult(StatusCodes.Status429TooManyRequests);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _currentNumOfConnections--;
        }
    }
}