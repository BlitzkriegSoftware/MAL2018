using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CustomerData.Repository;
using customerwebapi.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace customerwebapi
{
    /// <summary>
    /// Start Up
    /// </summary>
    public class Startup
    {

        private const string CorsPolicyName = "AllowAll";

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }


        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var filePath = Path.Combine(System.AppContext.BaseDirectory, "customerwebapi.xml");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvcCore().AddApiExplorer();
                       
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Customer API", Version = "v1" });
                c.IncludeXmlComments(filePath);

                c.DescribeAllEnumsAsStrings();
                c.UseReferencedDefinitionsForEnums();
            });

            services.AddCors(options => {
                options.AddPolicy(CorsPolicyName,
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            ;
                    });
            });

            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }

        // 
        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="env">IHostingEnvironment</param>
        /// <param name="logger">ILogger</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<string> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePages(async context =>
                {
                    context.HttpContext.Response.ContentType = "text/plain";
                    await context.HttpContext.Response.WriteAsync("Status code page, status code: " + context.HttpContext.Response.StatusCode);
                });
            }

            app.UseHttpsRedirection();
            app.UseHsts();

            app.ConfigureExceptionHandler(logger);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer API V1");
                c.DocumentTitle = "Modern Apps Live 2018, Orlando Fla";
            });

            app.UseCors(CorsPolicyName);

            app.UseMvc();
        }
    }
}
