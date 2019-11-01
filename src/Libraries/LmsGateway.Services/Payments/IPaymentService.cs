
using LmsGateway.Core.Payments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LmsGateway.Services.Payments
{
    public interface IPaymentService
    {
        List<IPaymentMethod> LoadAllPaymentMethods();
        List<IPaymentMethod> LoadAllActivePaymentMethods();
        IPaymentMethod LoadPaymentMethodByName(string name);
        Task ProcessPayment(ProcessPaymentRequest processPaymentRequest);

    }



}
