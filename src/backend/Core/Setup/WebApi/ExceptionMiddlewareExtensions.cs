using Core.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;
using System.Text.Json;

namespace Core.Setup.WebApi
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var (statusCode, message) = ConvertException(contextFeature);

                        context.Response.StatusCode = statusCode;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsJsonAsync(new
                        {
                            StatusCode = statusCode,
                            Message = message
                        });
                    }
                });
            });
        }

        private static (int statusCode, string message) ConvertException(IExceptionHandlerFeature handler)
        {
            switch (handler.Error)
            {
                case ApplicationErrorException:
                    return (500, handler.Error.Message);
                case ClientInputErrorException:
                    return (400, handler.Error.Message);
                case ValidationException:
                    return (400, handler.Error.Message);
                case ItemNotFoundException:
                    return (404, handler.Error.Message);
                case UnauthenticatedException:
                    return (401, handler.Error.Message);
                case UnauthorizedException:
                    return (403, handler.Error.Message);
                default:
                    return (500, "Unknown error");
            }
        }
    }
}
