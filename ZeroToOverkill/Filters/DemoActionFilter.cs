using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ZeroToOverkill.Filters
{
    public class DemoActionFilter : IActionFilter
    {
        private readonly ILogger<DemoActionFilter> _logger;

        public DemoActionFilter(ILogger<DemoActionFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

            _logger.LogInformation("After executing action {action} .",context.ActionDescriptor.DisplayName);

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Before executing" +
                                   " action {action} with arguments \"{@arguments}\"",
                context.ActionDescriptor.DisplayName, context.ActionArguments);

        }
    }
}
