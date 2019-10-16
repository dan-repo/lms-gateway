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
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using LmsGateway.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;

namespace LmsGateway.Web
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IList<PluginInfo> plugins = new List<PluginInfo>();

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
            LoadInstalledPlugins();

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

            //if (!_hostingEnvironment.IsDevelopment())
            //{
            //    services.Configure<MvcOptions>(options =>
            //    {
            //        options.Filters.Add(new RequireHttpsAttribute());
            //    });
            //}

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new PluginViewLocationExpander());
            });


            // Add framework services.
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
            

            //var mvcBuilder = services.AddMvc();
            //foreach (var plugin in plugins)
            //{
            //    // Register controller from modules
            //    mvcBuilder.AddApplicationPart(plugin.Assembly);

            //    // Register dependency in modules
            //    var dependencyRegistrar = plugin.Assembly.GetTypes().Where(x => typeof(IDependencyRegistrar).IsAssignableFrom(x)).FirstOrDefault();
            //    if (dependencyRegistrar != null && dependencyRegistrar != typeof(IDependencyRegistrar))
            //    {
            //        var dependencyRegistrarInstance = (IDependencyRegistrar)Activator.CreateInstance(dependencyRegistrar);
            //        dependencyRegistrarInstance.Register(services);
            //    }
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

            app.UseStaticFiles();
            app.UseIdentity();


            // Serving static file for modules
            foreach (var plugin in plugins)
            {
                var wwwrootDir = new DirectoryInfo(Path.Combine(plugin.Path, "wwwroot"));
                if (!wwwrootDir.Exists)
                {
                    continue;
                }

                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(wwwrootDir.FullName),
                    RequestPath = new Microsoft.AspNetCore.Http.PathString("/" + plugin.SortName)
                });
            }

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

        private void LoadInstalledPlugins()
        {
            var pluginRootFolder = _hostingEnvironment.ContentRootFileProvider.GetDirectoryContents("/Plugins");
            foreach (var pluginFolder in pluginRootFolder.Where(x => x.IsDirectory))
            {
                var binFolder = new DirectoryInfo(Path.Combine(pluginFolder.PhysicalPath, "bin"));
                if (!binFolder.Exists)
                {
                    continue;
                }

                foreach (var file in binFolder.GetFileSystemInfos("*.dll", SearchOption.AllDirectories))
                {
                    Assembly assembly;
                    try
                    {
                        assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                    }
                    catch (FileLoadException ex)
                    {
                        if (ex.Message == "Assembly with same name is already loaded")
                        {
                            continue;
                        }
                        throw;
                    }

                    if (assembly.FullName.Contains(pluginFolder.Name))
                    {
                        plugins.Add(new PluginInfo { Name = pluginFolder.Name, Assembly = assembly, Path = pluginFolder.PhysicalPath });
                    }
                }
            }


        }




    }
}
