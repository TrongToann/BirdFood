using BirdFood.Application.Exceptions;
using BirdFood.Domain.Exceptions;
using System.Text.Json;

namespace BirdFood.API.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }catch (Exception e) 
            {
                _logger.LogError(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            var respone = new
            {
                title = GetTitle(exception),
                status = statusCode,
                detail = exception.Message,
                errors = GetError(exception)
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(respone));
        }

        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };
        

        private static string GetTitle(Exception exception) =>
            exception switch
            {
                DomainException applicationException => applicationException.Title,
                _ => "Internal Server Error"
            };
        private static IReadOnlyCollection<BirdFood.Application.Exceptions.ValidationError> GetError(Exception exception)
        {
            IReadOnlyCollection<ValidationError> errors = null;
            if(exception is BirdFood.Application.Exceptions.ValidationException validationException) 
            {
                errors = validationException.Errors;
            }
            return errors;
        }
    }
}
