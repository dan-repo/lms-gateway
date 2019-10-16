using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LmsGateway.Paystack.Controllers
{
    public class PaystackController : Controller
    {

        public async Task<IActionResult> PaymentInfo()
        {
            return await Task.FromResult(View());
        }

        public async Task<IActionResult> Configure()
        {
            return await Task.FromResult(View());
        }

        




    }
}
