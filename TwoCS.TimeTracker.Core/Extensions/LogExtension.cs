namespace TwoCS.TimeTracker.Core.Extensions
{
    using System.Collections.Generic;
    using System.Net;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using TwoCS.TimeTracker.Core.Helpers;

    public static class LogExtension
    {
        public static IApplicationBuilder UseLogHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(options => {
                options.Run(
                async context =>
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>();

                    ErrorResult error = null;

                    if (exception.Error is BadRequestException customError)
                    {
                        error = new ErrorResult
                        {
                            Message = customError.Message,
                            Errors = customError.Errors
                        };
                    }
                    else
                    {
                        error = new ErrorResult
                        {
                            Message = exception.Error.Message,
                            Errors = new List<string>
                            {
                                exception.Error.Source
                            }
                        };
                    }

                    context.Response.Clear();

                    context.Response.StatusCode = 200;

                    context.Response.ContentType = "application/json";

                    var errorJson = error.ObjToJson();

                    await context.Response.WriteAsync(errorJson)
                        .ConfigureAwait(false);

                });
            });

            app.UseStatusCodePages(options => {
                options.Run(
                async context =>
                {
                    var respone = context.Response;

                    if (respone.StatusCode == (int)HttpStatusCode.Unauthorized ||
                        respone.StatusCode == (int)HttpStatusCode.Forbidden)
                    {
                        var error = new ErrorResult()
                        {
                            Message = "Not authorized."
                        };

                        context.Response.Clear();

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                        context.Response.ContentType = "application/json";

                        var errorJson = error.ObjToJson();

                        await context.Response.WriteAsync(errorJson)
                            .ConfigureAwait(false);
                    }
                });
            });

            return app;
        }

        public class ErrorResult
        {
            public string Message { get; set; }

            public IEnumerable<string> Errors { get; set; }
        }
    }
}
