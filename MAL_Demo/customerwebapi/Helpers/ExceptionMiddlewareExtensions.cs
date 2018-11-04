using Common;
using Common.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

// https://code-maze.com/global-error-handling-aspnetcore/

namespace customerwebapi.Helpers
{
    /// <summary>
    /// Extension: Exception Middleware
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Configure Exception Handler Middleware Method
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="logger"></param>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger<string> logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
                    var message = "Internal Error";

                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var ex = contextFeature.Error;

                        TypeSwitch.Do(ex,
                            TypeSwitch.Case<ArgumentException>(() => { statusCode = HttpStatusCode.BadRequest;  message = ex.Message;  }),
                            TypeSwitch.Case<ArgumentNullException>(() => { statusCode = HttpStatusCode.BadRequest; message = ex.Message; }),
                            TypeSwitch.Case<ArgumentOutOfRangeException>(() => { statusCode = HttpStatusCode.BadRequest; message = ex.Message;  }),
                            TypeSwitch.Case<BsValidationException>(() => { statusCode = HttpStatusCode.BadRequest; message = ((BsValidationException)ex).ValidationText("\n"); })
                        );

                        logger.LogError($"Exception: {ex}");

                        await context.Response.WriteAsync(new ErrorPayload()
                        {
                            StatusCode = (int)statusCode,
                            Message = message,
                            StackTrace = ex.StackTrace
                        }.ToString());
                    }
                });
            });
        }
    }
}
