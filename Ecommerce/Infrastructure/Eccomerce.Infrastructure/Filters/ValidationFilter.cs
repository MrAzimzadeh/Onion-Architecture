using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Eccomerce.Infrastructure.Filters;

// Custom Filter Service 
public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Eger gecersiz bir durum varsa  clienta geri donderirik 
        if (!context.ModelState.IsValid)
        {
            var error = context.ModelState
                .Where(x => x.Value.Errors.Any())
                .ToDictionary
                (e => e.Key,
                    e => e.Value.Errors.Select(e => e.ErrorMessage))
                .ToArray();
            context.Result = new BadRequestObjectResult(error);
            return;
        }

        // Her validasya ucun bu isleyeceyine gore biz her defe next etmeliyik 
        await next();
    }
}