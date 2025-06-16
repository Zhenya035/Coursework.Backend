using System.Text.Json;
using Coursework.Domain.Exceptions;

namespace Coursework.WebAPI.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var statusCode = StatusCodes.Status500InternalServerError;
        var message = "An unexpected error occurred.";
        string detailed;

        switch (exception)
        {
            case AlreadyAddedException alrAddEx:
                statusCode = StatusCodes.Status409Conflict;
                message = "Resource already exists.";
                detailed = alrAddEx.Message;
                break;
            
            case InvalidInputDataException invInpEx:
                statusCode = StatusCodes.Status400BadRequest;
                message = "Invalid input.";
                detailed = invInpEx.Message;
                break;
            
            case InvalidPasswordException invPassEx:
                statusCode = StatusCodes.Status401Unauthorized;
                message = "Authentication failed.";
                detailed = invPassEx.Message;
                break;
            
            case NotFoundException nFEx:
                statusCode = StatusCodes.Status404NotFound;
                message = "Resource not found.";
                detailed = nFEx.Message;
                break;

            default:
                detailed = exception.Message;
                break;
        }

        context.Response.StatusCode = statusCode;

        var response = new
        {
            StatusCode = statusCode,
            Message = message,
            Detailed = detailed
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}