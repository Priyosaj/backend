using System.Net;
using System.Text.Json;
using Priyosaj.Api.Errors;
using Priyosaj.Contacts.Utils;

namespace Priyosaj.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _env = env;
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            var statusCode = ex is ABaseException e ? e.StatusCode : 500;

            var response = _env.IsDevelopment()
                ? new ApiErrorResponse(statusCode, ex.Message,
                    ex.StackTrace?.ToString())
                : new ApiErrorResponse(statusCode);

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}