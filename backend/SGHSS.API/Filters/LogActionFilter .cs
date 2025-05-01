using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters
{
    public class LogActionFilter : IActionFilter
    {
        private readonly ILogger<LogActionFilter> _logger;

        public LogActionFilter(ILogger<LogActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var className = context.Controller.GetType().Name;
            var fullActionName = context.ActionDescriptor.DisplayName;

            var methodName = fullActionName?.Split('.').Last().Split(' ').First() ?? fullActionName;

            _logger.LogInformation($"Started execution of method {className}.{methodName}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var className = context.Controller.GetType().Name;
            var fullActionName = context.ActionDescriptor.DisplayName;

            var methodName = fullActionName?.Split('.').Last().Split(' ').First() ?? fullActionName;

            _logger.LogInformation($"Finished execution of method {className}.{methodName}");
        }
    }
}
