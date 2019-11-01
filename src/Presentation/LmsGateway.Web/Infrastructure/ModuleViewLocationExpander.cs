using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Razor;

namespace LmsGateway.Web.Infrastructure
{
    public class ModuleViewLocationExpander : IViewLocationExpander
    {
        private const string _pluginKey = "module";

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            var controller = context.ActionContext.ActionDescriptor.DisplayName;
            var moduleName = controller.Split('.')[1];
            if (moduleName != "Web")
            {
                context.Values[_pluginKey] = moduleName;
            }

            if (controller.Contains("Areas"))
            {
                context.Values[_pluginKey] = "area";
            }
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            string action = context.ActionContext.ActionDescriptor.DisplayName;
            string projectName = action.Split('.')[1];
            string baseName = action.Split('.')[0];

            if (context.Values.ContainsKey(_pluginKey))
            {
                var module = context.Values[_pluginKey];
                if (!string.IsNullOrWhiteSpace(module))
                {
                    if (module == "area")
                    {
                        var moduleViewLocations = new string[]
                           {
                        "/Modules/"+ baseName + ".Paystack/Views/Paystack/{0}.cshtml",
                        "/Modules/"+ baseName + ".Paystack/Views/Shared/{0}.cshtml"
                           };

                        viewLocations = moduleViewLocations.Concat(viewLocations);
                    }
                    else
                    {
                        var moduleViewLocations = new string[]
                            {
                        "/Modules/"+ baseName + "." + projectName + "/Views/{1}/{0}.cshtml",
                        "/Modules/"+ baseName + "." + projectName + "/Views/Shared/{0}.cshtml"
                            };

                        viewLocations = moduleViewLocations.Concat(viewLocations);
                    }
                }
            }
            
            return viewLocations;
        }



    }
}
