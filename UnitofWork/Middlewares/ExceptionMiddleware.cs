using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UnitofWork.Errors;

namespace UnitofWork.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task Invoke (HttpContext context)
        {

            try 
            {
                await next(context);
            }
            catch(Exception ex)
            {
                ////for default manual middlware error
                ///
                //logger.LogError(ex, ex.Message);
                //context.Response.StatusCode = 500;
                //await context.Response.WriteAsync(ex.Message);


                // customised error desctiption
                //create ApiError.cs
                ApiError response;
                HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

                string message;

                var exceptionType = ex.GetType();

                if(exceptionType == typeof(UnauthorizedAccessException))
                {
                    statusCode = HttpStatusCode.Forbidden;
                    message = "You are not authorized";
                }
                else
                {
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "Some unknown error occureds";
                }

                if(env.IsDevelopment())
                {

                    response = new ApiError((int)statusCode, ex.Message,ex.StackTrace.ToString());
                }
                else
                {
                    response = new ApiError((int)statusCode, message, "null");
                }

                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(response.ToString());


            }
        }
    }
}

//https://www.youtube.com/watch?v=pOsExnj-_Kg&list=PL_NVFNExoAxclqXo9fLAeP0G2Qp56Fu8C&index=40
//https://www.youtube.com/watch?v=FS7U0KjQLlE&list=PL_NVFNExoAxclqXo9fLAeP0G2Qp56Fu8C&index=41