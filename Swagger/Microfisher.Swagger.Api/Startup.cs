using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Microfisher.Swagger.Api
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Crypto Exchange",
                    Description = "基于.NET Core 3.0 的区块链数字货币交易所",
                    Contact = new OpenApiContact
                    {
                        Name = "Microfisher",
                        Email = "276679490@qq.com",
                        Url = new Uri("http://cnblogs.com/microfisher"),
                    },
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

                var baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                var xmlFile = System.AppDomain.CurrentDomain.FriendlyName + ".xml";
                var xmlPath = Path.Combine(baseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crypto Exchange");
                c.RoutePrefix = "swagger";
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}



//var securityScheme = new OpenApiSecurityScheme()
//{
//    Description = appSettings.Swagger.Security.Description,
//    Name = appSettings.Swagger.Security.Name,
//    In = ParameterLocation.Header,
//    Type = SecuritySchemeType.ApiKey,
//    BearerFormat = appSettings.Swagger.Security.BearerFormat,
//    Scheme = appSettings.Swagger.Security.Scheme
//};
//c.AddSecurityDefinition(appSettings.Swagger.Security.Scheme, securityScheme);

//                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
//                    {
//                        new OpenApiSecurityScheme
//                        {
//                            Reference = new OpenApiReference {
//                                Type = ReferenceType.SecurityScheme,
//                                Id = appSettings.Swagger.Security.Scheme
//                            }
//                        },
//                        new string[] { }
//                    }
//                });

//                //c.EnableAnnotations();

//                //c.IgnoreObsoleteActions(); // 忽略过时的接口[Obsolete]

//                //c.(api => api.HttpMethod); // 对action根据Http请求进行分组

//                //c.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}"); // 指定接口排序规则

//                c.IncludeXmlComments(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Microfisher.Reactor.Services.xml"));
//                c.IncludeXmlComments(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, System.AppDomain.CurrentDomain.FriendlyName + ".xml"));