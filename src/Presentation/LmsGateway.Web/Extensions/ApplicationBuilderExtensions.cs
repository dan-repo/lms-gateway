using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.IO;
using Microsoft.AspNetCore.Builder;
using LmsGateway.Core.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using System.Runtime.Loader;

namespace LmsGateway.Web.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        private static IHostingEnvironment _hostingEnvironment;
        private static readonly IList<ModuleInfo> modules = new List<ModuleInfo>();

        public static void UseModuleStaticFiles(this IApplicationBuilder app, IHostingEnvironment hostingEnvironment)
        {
            Guard.NotNull(hostingEnvironment, nameof(hostingEnvironment));

            _hostingEnvironment = hostingEnvironment;

            // Serving static file for modules
            LoadInstalledModules();
            foreach (var module in modules)
            {
                var wwwrootDir = new DirectoryInfo(Path.Combine(module.Path, "wwwroot"));
                if (!wwwrootDir.Exists)
                {
                    continue;
                }

                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(wwwrootDir.FullName),
                    RequestPath = new Microsoft.AspNetCore.Http.PathString("/" + module.SortName)
                });
            }
        }

        private static void LoadInstalledModules()
        {
            var moduleRootFolder = _hostingEnvironment.ContentRootFileProvider.GetDirectoryContents("/Modules");
            foreach (var moduleFolder in moduleRootFolder.Where(x => x.IsDirectory))
            {
                var binFolder = new DirectoryInfo(Path.Combine(moduleFolder.PhysicalPath, "bin"));
                if (!binFolder.Exists)
                {
                    continue;
                }

                foreach (var file in binFolder.GetFileSystemInfos("*.dll", SearchOption.AllDirectories))
                {
                    Assembly assembly = null;
                    try
                    {
                        assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                    }
                    catch (FileLoadException ex)
                    {
                        if (ex.Message == "Assembly with same name is already loaded")
                        {
                            modules.Add(new ModuleInfo { Name = moduleFolder.Name, Assembly = assembly, Path = moduleFolder.PhysicalPath });
                            continue;
                        }
                        throw;
                    }

                    if (assembly.FullName.Contains(moduleFolder.Name))
                    {
                        modules.Add(new ModuleInfo { Name = moduleFolder.Name, Assembly = assembly, Path = moduleFolder.PhysicalPath });
                    }
                }
            }


        }


    }
}
