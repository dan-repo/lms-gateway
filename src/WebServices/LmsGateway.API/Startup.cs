using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace LmsGateway.API
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostEnvironment;

        public Startup(IHostingEnvironment env)
        {
            _hostEnvironment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "MyAPI",
                    Description = "Testing"
                });
            });
            
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                // keeping swagger UI URL consistent with my previous settings
                //c.RoutePrefix = "swagger/ui";
                c.RoutePrefix = "help";

                if (_hostEnvironment.IsProduction())
                {
                    // adding endpoint to JSON file containing API endpoints for UI
                    c.SwaggerEndpoint("/lms-api/swagger/v1/swagger.json", "API v1"); //url: http://localhost:4847/swagger/ui
                }
                else
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"); //url: http://localhost:4847/swagger/ui
                }

                //// disabling the Swagger validator -- passing null as validator URL.
                //// Alternatively, you can specify your own internal validator
                //c.EnabledValidator(null);
            });

            app.UseMvc();
        }
    }
}
