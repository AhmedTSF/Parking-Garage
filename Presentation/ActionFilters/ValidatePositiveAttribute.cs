using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.ActionFilters;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class ValidatePositiveAttribute : ActionFilterAttribute
{
    private readonly string[] _parametersToCheck;

    public ValidatePositiveAttribute(params string[] parametersToCheck)
    {
        _parametersToCheck = parametersToCheck;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var paramName in _parametersToCheck)
        {
            if (context.ActionArguments.TryGetValue(paramName, out var value))
            {
                if (value is int intValue && intValue <= 0)
                {
                    context.Result = new BadRequestObjectResult(new
                    {
                        Error = $"Parameter '{paramName}' must be greater than zero."
                    });
                    return;
                }
                else if (value is long longValue && longValue <= 0)
                {
                    context.Result = new BadRequestObjectResult(new
                    {
                        Error = $"Parameter '{paramName}' must be greater than zero."
                    });
                    return;
                }
            }
        }
    }
}
