using System.Net.Mime;
using System.Text.Json;
using MetaITAPI.Utils.Exceptions;
using MetaITAPI.Utils.Responses;

namespace MetaITAPI.Utils.Middlewares;

public class ExceptionMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(
        ILogger<ExceptionMiddleware> logger,
        RequestDelegate next)
    {
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
            await HandleCustomExceptionResponseAsync(context, ex);
        }
    }

    private async Task HandleCustomExceptionResponseAsync(
        HttpContext context,
        Exception ex)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;

        if (ex is NotExistException)
            context.Response.StatusCode = StatusCodes.Status404NotFound;
        else if (ex is AlreadyExistException)
            context.Response.StatusCode = StatusCodes.Status409Conflict;
        else if (ex is InvalidInputException)
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        else
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new ErrorResponse(
            context.Response.StatusCode,
            ex.Message,
            ex.StackTrace?.ToString());

        var options = new JsonSerializerOptions
            { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var json = JsonSerializer.Serialize(response, options);
        await context.Response.WriteAsync(json);
    }
}
