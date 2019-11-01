using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Web.Framework.UI
{
    public class WidgetProvider : IWidgetProvider
    {
        public WidgetProvider()
        {
            Widgets = new List<WidgetZone>();
        }

        public List<WidgetZone> Widgets { get; set; } 

        public void Register(WidgetZone widgetZone)
        {
            Widgets.Add(widgetZone);
        }

        public bool IsRegistered(WidgetZone widgetZone)
        {
           return  Widgets.Contains(widgetZone);
        }




    }
}
