using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using LmsGateway.Core.Infrastructure;
using LmsGateway.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using LmsGateway.Core.Notifications;
using LmsGateway.Services.Notifications;

namespace LmsGateway.Web
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

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
            EmailServer emailServer = GetEmailServer();
            
            IDictionary<string, string> connectionStrings = new Dictionary<string, string>()
            {
                ["Identity"] = Configuration["Identity:ConnectionString"],
                ["Data"] = Configuration["Data:ConnectionString"]
            };

            // Add application services.
            services.AddTransient<ITypeFinder, AppTypeFinder>();
            services.RegisterCustomServices(connectionStrings);
            services.Add(new ServiceDescriptor(typeof(EmailServer), emailServer));
            services.AddTransient<IEmailService, EmailService>();

            if (!_env.IsDevelopment())
            {
                services.Configure<MvcOptions>(options =>
                {
                    options.Filters.Add(new RequireHttpsAttribute());
                });
            }


            //// Only allow authenticated users.
            //var defaultPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
            //    .RequireAuthenticatedUser()
            //    .Build();

            //// Add MVC services to the services container.
            //services.AddMvc(setup =>
            //{
            //    setup.Filters.Add(new Microsoft.AspNetCore.Mvc.Authorization.AuthorizeFilter(defaultPolicy));
            //});


            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "areaRoute",
                   template: "{area:exists}/{controller}/{action}/{id?}",
                   defaults: new { controller = "Dashboard", action = "Index" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private EmailServer GetEmailServer()
        {
            string name = Configuration["EmailServer:Name"];
            string host = Configuration["EmailServer:Host"];
            string username = Configuration["EmailServer:Username"];
            string password = Configuration["EmailServer:Password"];
            int port = Convert.ToInt32(Configuration["EmailServer:Port"]);
            bool useSsl = Convert.ToBoolean(Configuration["EmailServer:UseSsl"]);

            return new EmailServer(name, username, password, host, port, useSsl);
        }




    }
}
