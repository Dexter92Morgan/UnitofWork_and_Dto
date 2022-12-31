using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using UnitofWork.Middlewares;

namespace UnitofWork.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }

            public static void ConfigureBuiltinExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {

                app.UseExceptionHandler(
                    options =>
                    {
                        options.Run(
                                async context =>
                                {
                                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                    var ex = context.Features.Get<IExceptionHandlerFeature>();
                                    if (ex != null)
                                    {

                                        await context.Response.WriteAsync(ex.Error.Message);
                                    }
                                }
                            );
                    });
            }
        }
    }
}

//https://www.youtube.com/watch?v=pOsExnj-_Kg&list=PL_NVFNExoAxclqXo9fLAeP0G2Qp56Fu8C&index=40
