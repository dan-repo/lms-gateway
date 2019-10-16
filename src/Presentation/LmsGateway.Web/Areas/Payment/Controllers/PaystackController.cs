using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LmsGateway.Web.Areas.Payment.Controllers
{
    [Authorize]
    [Area("Payment")]
    public class PaystackController : Controller
    {
        public PaystackController()
        {

        }

        public async Task<IActionResult> PaymentInfo()
        {
            return await Task.FromResult(View());
        }



    }
}
