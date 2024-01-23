using Application.Enums;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.RegularExpressions;

namespace Application.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AggregateException exception)
            {
                var firstexception = exception.Flatten()?.InnerExceptions?.FirstOrDefault();
                await HandleExceptionAsync(context, firstexception);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string Message = exception switch
            {
                FullCartException ex => ex.Message,
                _ => UnHandledException(context, exception)
            };
            await context.Response.WriteAsync(Message);
        }
        private string UnHandledException(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unexpected error occurred.");
            return ValidationKey.ServerError.ToString();
        }
    }
}
