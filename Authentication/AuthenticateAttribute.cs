using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Patikadev_RestfulApi.Services.Interfaces;

namespace Patikadev_RestfulApi.Authentication;

public class AuthenticateAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // API Key kontrolü
        if (!context.HttpContext.Request.Headers.TryGetValue("X-API-Key", out var apiKey))
        {
            context.Result = new UnauthorizedObjectResult(new { message = "API Key is required" });
            return;
        }

        // API Key'in geçerli olup olmadığını kontrol eder
        var authService = context.HttpContext.RequestServices.GetRequiredService<IAuthService>();
        if (!authService.ValidateApiKey(apiKey))
        {
            context.Result = new UnauthorizedObjectResult(new { message = "Invalid API Key" });
            return;
        }
    }
}