using System;
using LmsGateway.Core.Payments;
using Microsoft.Extensions.DependencyInjection;
using LmsGateway.Core.Infrastructure;
using System.Reflection;
using System.Collections.Generic;
using LmsGateway.Core.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly List<IPaymentMethod> _paymentProviders;

        public PaymentService(IServiceProvider serviceProvider)
        {
            Guard.NotNull(serviceProvider, nameof(serviceProvider));
                       
            _paymentProviders = serviceProvider.GetServices<IPaymentMethod>().ToList();
        }

        public List<IPaymentMethod> LoadAllPaymentMethods()
        {
            return _paymentProviders;
        }

        public List<IPaymentMethod> LoadAllActivePaymentMethods()
        {
            return _paymentProviders.Where(x => x.IsActive == true).ToList();
        }

        public IPaymentMethod LoadPaymentMethodByName(string name)
        {
            return _paymentProviders.Where(x => x.Metadata.Name == name).SingleOrDefault();
        }

        public async Task ProcessPayment(ProcessPaymentRequest processPaymentRequest)
        {
            string paymentMethodName = processPaymentRequest.PaymentMethodName;

            if (paymentMethodName.HasValue())
            {
                IPaymentMethod paymentMethod = LoadPaymentMethodByName(paymentMethodName);

                Guard.NotNull(paymentMethod, "Could not load method!");

                await paymentMethod.PostProcessPayment(processPaymentRequest);
            }
        }



    }
}
