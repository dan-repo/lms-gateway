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
using LmsGateway.Web.Extensions;
using LmsGateway.Web.Framework.Extensions;
using Microsoft.AspNetCore.Mvc;
using LmsGateway.Core.Notifications;
using LmsGateway.Services.Notifications;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using LmsGateway.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using LmsGateway.Core.Configuration;
using LmsGateway.Services.Configuration;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace LmsGateway.Web
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public Startup(IHostingEnvironment env)
        {
            _hostingEnvironment = env;

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
            services.Add(new ServiceDescriptor(typeof(EmailServer), emailServer));
            services.AddOtherServices(connectionStrings);

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);
            services.AddResponseCompression(options =>
            {
                // options.EnableForHttps = true;
                // options.MimeTypes = new string[] { "multipart/form-data", "application/pdf" };
                options.Providers.Add<GzipCompressionProvider>();
            });

            //if (!_hostingEnvironment.IsDevelopment())
            //{
            //    services.Configure<MvcOptions>(options =>
            //    {
            //        options.Filters.Add(new RequireHttpsAttribute());
            //    });
            //}

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ModuleViewLocationExpander());
            });


            // Add framework services.
            //services.AddMvc();

            
            services.AddMvc().AddMvcOptions(options =>
            {
                options.Filters.AddService(typeof(Paystack.Filters.PaystackRegistrationConfirmFilter));
                options.Filters.AddService(typeof(Paystack.Filters.PaystackRegistrationCompletedFilter));
            });
            services.AddMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            //services.AddPluginDependencies(plugins, connectionStrings);

            //var mvcBuilder = services.AddMvc();
            //foreach (var plugin in plugins)
            //{
            //    // Register controller from modules
            //    mvcBuilder.AddApplicationPart(plugin.Assembly).AddControllersAsServices();

            //    //// Register dependency in modules
            //    //var dependencyRegistrar = plugin.Assembly.GetTypes().Where(x => typeof(IDependencyRegistrar).IsAssignableFrom(x)).FirstOrDefault();
            //    //if (dependencyRegistrar != null && dependencyRegistrar != typeof(IDependencyRegistrar))
            //    //{
            //    //    var dependencyRegistrarInstance = (IDependencyRegistrar)Activator.CreateInstance(dependencyRegistrar);
            //    //    dependencyRegistrarInstance.Register(services, connectionStrings);
            //    //}
            //}
            
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


            app.UseResponseCompression();

            app.UseStaticFiles();
            app.UseModuleStaticFiles(env);
            app.UseIdentity();
            app.UseSession();
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
