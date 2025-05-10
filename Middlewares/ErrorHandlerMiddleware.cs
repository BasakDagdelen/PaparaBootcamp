using System.Net;
using System.Text.Json;

namespace Patikadev_RestfulApi.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var errorResponse = new
            {
                message = error.Message,
                statusCode = HttpStatusCode.InternalServerError
            };

            switch (error)
            {
                case KeyNotFoundException e:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse = new { message = "Record not found", statusCode = HttpStatusCode.NotFound };
                    break;
                case ArgumentException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse = new { message = "Unauthorized access", statusCode = HttpStatusCode.Unauthorized };
                    break;
                case UnauthorizedAccessException e:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    errorResponse = new { message = "An error occurred", statusCode = HttpStatusCode.InternalServerError };
                    break;
            }

            _logger.LogError(error, "An error occurred: {Message}", error.Message);

            var result = JsonSerializer.Serialize(errorResponse);
            await response.WriteAsync(result);
        }
    }
}
