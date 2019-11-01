using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Web.Framework.UI
{
    public interface IWidgetProvider
    {
        List<WidgetZone> Widgets { get; set; }
        void Register(WidgetZone widgetZone);
        bool IsRegistered(WidgetZone widgetZone);
    }


}
