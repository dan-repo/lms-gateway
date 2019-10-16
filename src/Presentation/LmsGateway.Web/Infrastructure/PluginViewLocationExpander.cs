using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Razor;

namespace LmsGateway.Web.Infrastructure
{
    public class PluginViewLocationExpander : IViewLocationExpander
    {
        private const string _pluginKey = "plugin";

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            string action = context.ActionContext.ActionDescriptor.DisplayName;
            string projectName = action.Split('.')[1];
            string baseName = action.Split('.')[0];

            if (context.Values.ContainsKey(_pluginKey))
            {
                var plugin = context.Values[_pluginKey];
                if (!string.IsNullOrWhiteSpace(plugin))
                {
                    var moduleViewLocations = new string[]
                    {
                        "/Plugins/"+ baseName + "." + projectName + "/Views/{1}/{0}.cshtml",
                        "/Plugins/"+ baseName + "." + projectName + "/Views/Shared/{0}.cshtml"

                        //"/Plugins/LmsGateway." + plugin + "/Views/{1}/{0}.cshtml",
                        //"/Plugins/LmsGateway." + plugin + "/Views/Shared/{0}.cshtml"
                    };

                    viewLocations = moduleViewLocations.Concat(viewLocations);
                }
            }
            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            var controller = context.ActionContext.ActionDescriptor.DisplayName;
            var pluginName = controller.Split('.')[2];
            if (pluginName != "Web")
            {
                context.Values[_pluginKey] = pluginName;
            }
        }


        //C:\Users\LENOVO\Documents\Repositories\lms-gateway\src\Presentation\LmsGateway.Web\Plugins\LmsGateway.Paystack\Views\Paystack\PaymentInfo.cshtml
        //C:\Users\LENOVO\Documents\Repositories\lms-gateway\src\Presentation\LmsGateway.Web\Views\Home\Index.cshtml
    }
}
