
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using LmsGateway.Core.Infrastructure;
using LmsGateway.Web.Framework.UI;
using LmsGateway.Paystack.Interfaces;

namespace LmsGateway.Paystack.Filters
{
    public class PaystackRegistrationCompletedFilter : ResultFilterAttribute
    {
        private readonly IGatewayLuncher _gatewayLuncher;
        private readonly IWidgetProvider _widgetProvider;

        public PaystackRegistrationCompletedFilter(IWidgetProvider widgetProvider, IGatewayLuncher gatewayLuncher)
        {
            Guard.NotNull(gatewayLuncher, nameof(gatewayLuncher));
            Guard.NotNull(widgetProvider, nameof(widgetProvider));

            _gatewayLuncher = gatewayLuncher;
            _widgetProvider = widgetProvider;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            // should only run on a full view rendering result
            var result = context.Result as IActionResult;
            if (result == null)
                return;

            var action = context.RouteData.Values["action"] as string;
            var controller = context.RouteData.Values["controller"] as string;

            if (action.ToLower() == "completed" && controller.ToLower() == "registration")
            {
                _widgetProvider.Register(WidgetZone.PaymentGatewayResponse);
            }
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            if (_gatewayLuncher.IsSuccessful == false && _gatewayLuncher.HasLunchedPaymentPage == true)
            {
                //var ad = new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor();

                //context.ActionDescriptor.DisplayName = "completed";

                //IDictionary<string, string> routeValues = new Dictionary<string, string>()
                //{
                //    { "action", "completed" },
                //    { "controller", "registration" },
                //    { "area", "student" },
                //};
                //context.ActionDescriptor.RouteValues = routeValues;
                
            }

            //base.OnResultExecuted(context);
        }



    }
}
