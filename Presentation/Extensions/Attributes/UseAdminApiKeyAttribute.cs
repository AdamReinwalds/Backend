using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Extensions.Attributes
{
    public class UseAdminApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var adminKey = configuration["SecretKeys:Admin"] ?? null;

            if (string.IsNullOrEmpty(adminKey) || !context.HttpContext.Request.Headers.TryGetValue("X-ADM-API-KEY", out var providedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("Api-key is missing.");
                return;
            }
            if (!string.Equals(providedApiKey, adminKey))
            {
                context.Result = new UnauthorizedObjectResult("Invalid api-key.");
                return;
            }
            await next();
        }
    }
}
