using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ExceptionMiddleware
{
    public static class ExceptionHelper
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        Console.WriteLine($"Hata: {contextFeature.Error}");

                        //var result = JsonConvert.SerializeObject(new { error = contextFeature.Error.Message });
                        //await context.Response.WriteAsync(result)

                        await context.Response.WriteAsync(new ExceptionResponse
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message

                        }.ToString());
                    }
                });
            });
        }
    }
}
