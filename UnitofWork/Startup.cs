using Datas.Datacontext;
using Datas.Helpers_Automapper;
using Datas.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnitofWork.Extensions;
using UnitofWork.Middlewares;

namespace UnitofWork
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var sqlConnectionString = Configuration["Data:DefaultConnection:ConnectionString"];

            services.AddDbContext<DataContext>(options => options.UseNpgsql(sqlConnectionString));

            //Auto Mapper
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            //Dependency Injection
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //for Jwt token for authentication
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes("this is my top secret"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => {

                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = key


                    };
                });

            services.AddSwaggerGen();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Test API",
                    Description = "A simple example for swagger api information",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Preetham",
                        Email = "xyz@gmail.com",
                        Url = new Uri("https://example.com"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under OpenApiLicense",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                //to check token
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            });
            //for swagger token check
            //https://www.youtube.com/watch?v=IYWOWxw7dys

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","State API V1");
            
            });

            app.ConfigureExceptionHandler(env);

            //app.UseMiddleware<ExceptionMiddleware>();

            //app.ConfigureBuiltinExceptionHandler(env);

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

// reference link
//https://www.youtube.com/watch?v=W6H4kZkyS6U

//https://www.youtube.com/watch?v=nRkx-vBwsOA&list=PL_NVFNExoAxclqXo9fLAeP0G2Qp56Fu8C&index=39

// to create JWT
//https://www.youtube.com/watch?v=JFam_sgLwx8&list=PL_NVFNExoAxclqXo9fLAeP0G2Qp56Fu8C&index=45