using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Core.Payments
{
    public interface IPaymentMethod
    {
        bool IsActive { get; set; }
        PaymentMethodType PaymentMethodType { get; }
        PaymentMetadata Metadata { get; set; }

        Task PostProcessPayment(ProcessPaymentRequest processPaymentRequest);


        //ProcessPaymentResult ProcessPayment(ProcessPaymentRequest processPaymentRequest);
        //void GetPaymentInfoRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues);
        //Type GetControllerType();
        
    }
}
