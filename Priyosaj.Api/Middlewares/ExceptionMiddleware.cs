using System.Text.Json;
using Priyosaj.Core.Models;
using Priyosaj.Core.Utils;

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
            Console.WriteLine(ex);
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            
            var statusCode = ex is ABaseException e ? e.StatusCode : 500;
            context.Response.StatusCode = statusCode;
            var response = _env.IsDevelopment()
                ? new ApiErrorResponse(statusCode, ex.Message,
                    ex.StackTrace?.ToString())
                : new ApiErrorResponse(statusCode);

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}