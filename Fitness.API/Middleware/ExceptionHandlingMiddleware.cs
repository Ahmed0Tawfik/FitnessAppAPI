using FluentValidation;
using System.Net;
using System.Text.Json;

namespace Fitness.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex) // FluentValidation exceptions
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (Exception ex) // All other exceptions
            {
                await HandleGenericExceptionAsync(context, ex);
            }
        }

        private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
        {
            var response = new
            {
                Message = "Validation failed",
                Errors = ex.Errors.Select(err => new { err.PropertyName, err.ErrorMessage })
            };

            return WriteResponseAsync(context, HttpStatusCode.BadRequest, response);
        }

        private static Task HandleGenericExceptionAsync(HttpContext context, Exception ex)
        {
            var response = new
            {
                Message = "An unexpected error occurred",
                Error = ex.Message
            };

            return WriteResponseAsync(context, HttpStatusCode.InternalServerError, response);
        }

        private static Task WriteResponseAsync(HttpContext context, HttpStatusCode statusCode, object response)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
