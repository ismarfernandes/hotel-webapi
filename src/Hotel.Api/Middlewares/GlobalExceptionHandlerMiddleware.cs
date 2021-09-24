using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Hotel.Shared.Extensions;

using Microsoft.AspNetCore.Http;

namespace Hotel.Api.Middlewares
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errors = exception.InnerExceptionsMessages();
            var result = errors.FailureResponse();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = null
            };
            options.Converters.Add(new JsonStringEnumConverter());

            return context.Response.WriteAsync(JsonSerializer.Serialize(result, options));
        }
    }
}
