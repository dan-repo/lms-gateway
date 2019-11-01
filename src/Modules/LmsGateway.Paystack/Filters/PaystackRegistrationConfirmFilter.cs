using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using LmsGateway.Web.Framework.UI;
using LmsGateway.Core.Infrastructure;

namespace LmsGateway.Paystack.Filters
{
    public class PaystackRegistrationConfirmFilter : ResultFilterAttribute
    {
        private readonly IWidgetProvider _widgetProvider;

        public PaystackRegistrationConfirmFilter(IWidgetProvider widgetProvider)
        {
            Guard.NotNull(widgetProvider, nameof(widgetProvider));

            _widgetProvider = widgetProvider;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            // should only run on a full view rendering result
            var result = context.Result as IActionResult;
            if (result == null)
                return;

            //ViewResult viewResult = result as ViewResult;

            var action = context.RouteData.Values["action"] as string;
            var controller = context.RouteData.Values["controller"] as string;
            
            if (action.ToLower() == "confirm" && controller.ToLower() == "registration")
            {
                _widgetProvider.Register(WidgetZone.TransactionReference);
            }
        }

    }
}
