namespace Presentation.Extensions.Middlewares;
public class DefaultApiKeyMiddleWare(RequestDelegate next, IConfiguration configuration)
{
    private readonly RequestDelegate _next = next;
    private readonly IConfiguration _configuration = configuration;
    private const string ApiKeyHeaderName = "X-API-KEY";

    public async Task InvokeAsync(HttpContext context)
    {
        var defaultApiKey = _configuration["SecretKeys:Default"] ?? null;

        if (string.IsNullOrEmpty(defaultApiKey) || !context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var providedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Api-key is missing.");
            return;
        }
        if(!string.Equals(providedApiKey, defaultApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid api-key");
            return;
        }

        await _next(context);

    }
}
