using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using LmsGateway.Web.Areas.Student.Models;
using LmsGateway.Domain.Registrations;
using Microsoft.AspNetCore.Mvc.Rendering;
using LmsGateway.Services.Registrations;
using LmsGateway.Core.Infrastructure;
using System.Linq.Expressions;
using LmsGateway.Web.Areas.Student.Extensions;
using LmsGateway.Domain.Users;
using Microsoft.AspNetCore.Identity;
using LmsGateway.Core.Domain.Payments;
using LmsGateway.Services.Configuration;
using Newtonsoft.Json;
using LmsGateway.Core.Payments;
using LmsGateway.Services.Payments;
using LmsGateway.Core.Extensions;
//using Newtonsoft.Json;

namespace LmsGateway.Web.Areas.Student.Controllers
{
    [Authorize]
    [Area("Student")]
    public class RegistrationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IRegistrationService _registrationService;
        private readonly IRegistrationFeeService _registrationFeeService;
        private readonly IRegistrationPeriodService _registrationPeriodService;
        private readonly IPaymentService _paymentService;

        public RegistrationController(IRegistrationFeeService registrationFeeService, 
            IRegistrationPeriodService registrationPeriodService,
            IRegistrationService registrationService,
            IPaymentService paymentService,
            UserManager<User> userManager)
        {
            Guard.NotNull(userManager, nameof(userManager));
            Guard.NotNull(paymentService, nameof(paymentService));
            Guard.NotNull(registrationService, nameof(registrationService));
            Guard.NotNull(registrationFeeService, nameof(registrationFeeService));
            Guard.NotNull(registrationPeriodService, nameof(registrationPeriodService));

            _userManager = userManager;
            _paymentService = paymentService;
            _registrationService = registrationService;
            _registrationFeeService = registrationFeeService;
            _registrationPeriodService = registrationPeriodService;
        }

        public async Task<IActionResult> Index()
        {
            RegistrationModel model = new RegistrationModel();
            model.RegistrationPeriods =  await _registrationPeriodService.GetAll("Session,Semester");
            model.Student = await _userManager.GetUserAsync(User); //await _registrationFeeService.GetAll("Programme, Department, Level, RegistrationPeriod, RegistrationPeriod.Session, RegistrationPeriod.Semester");

            if (model.RegistrationPeriods != null && model.RegistrationPeriods.Count > 0)
            {
                model.Periods.Add(new SelectListItem() { Value = "", Text = "Select a period" });
                foreach (RegistrationPeriod period in model.RegistrationPeriods)
                {
                    model.Periods.Add(new SelectListItem { Text = period.Semester.Name + " - " + period.Session.Name, Value = period.Id.ToString() });
                }
            }
            else
            {
                ViewBag.CurrencyLoadMessage = "Period listing failed on load!";
            }
            
            return await Task.FromResult(View(model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["registrationModel"] = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(model));

                //await HttpContext.Session.SetComplexData("registrationModel", model);
                return await Task.FromResult(RedirectToAction(nameof(Confirm)));
            }

            ModelState.AddModelError("", "");

            return await Task.FromResult(View(model));
        }

        public async Task<IActionResult> Confirm()
        {
            string registrationModelJson = (string)TempData.Peek("registrationModel");
            RegistrationModel registrationModel = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<RegistrationModel>(registrationModelJson));


            //RegistrationModel registrationModel = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<RegistrationModel>(registrationModelJson));

            return await Task.FromResult(View(registrationModel));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Fee", "Invalid model!");
                return await Task.FromResult(View(model));
            }

            string paymentMethod = "Paystack";
            Registration registration = new Registration()
            {
                StudentId = model.Student.Id,
                RegistrationPeriodId = model.Period,
                RegistrationFeeId = model.Fee,
                RegisteredOn = DateTime.UtcNow,

                Details = new List<RegistrationDetail>()
                    {
                        new RegistrationDetail()
                        {
                            PaymentStatus = (int)PaymentStatus.Pending,
                            PaymentMethod = paymentMethod,
                            AmountPaid = model.AmountPayable,
                            RegisteredOn = DateTime.UtcNow,
                        }
                    }
            };

            Registration newRegistration = await _registrationService.RegisterAsync(registration);
            if (newRegistration != null && newRegistration.Id > 0)
            {
                model.Id = newRegistration.Id;
                ProcessPaymentRequest processPaymentRequest = new ProcessPaymentRequest()
                {
                    HttpContext = HttpContext,
                    Registration = newRegistration,
                    TransactionReference = model.TransactionReference,
                    PaymentStatus = PaymentStatus.Pending,
                    PaymentMethodName = paymentMethod,
                    SelectedCurrencyId = model.Currency,
                    AmountPayable = model.AmountPayable
                };

                await _paymentService.ProcessPayment(processPaymentRequest);
            }

            string error = HttpContext.Session.GetString("errorMessage");
            if (error.HasValue())
            {
                ModelState.AddModelError("Fee", error);
            }

            return await Task.FromResult(View(model));
        }

        public async Task<IActionResult> Completed()
        {
            string registrationModelJson = (string)TempData.Peek("registrationModel");
            RegistrationModel registrationModel = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<RegistrationModel>(registrationModelJson));

            return await Task.FromResult(View(registrationModel));
        }

        public async Task<IActionResult> Invoice()
        {
            string registrationModelJson = (string)TempData.Peek("registrationModel");
            RegistrationModel registrationModel = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<RegistrationModel>(registrationModelJson));

            return await Task.FromResult(View(registrationModel));
        }



        public async Task<JsonResult> GetRegistrationFeeByPeriodId(int periodId)
        {
            Expression<Func<RegistrationFee, bool>> selector = rf => rf.RegistrationPeriodId == periodId;
            List<RegistrationFee> registrationFees = await _registrationFeeService.GetByPeriodId(periodId, "Programme,Department,Level");

            List<SelectListItem> selectList = new List<SelectListItem>();
            IEnumerable<RegistrationFeeModel> registrationFeeModels = registrationFees.ToModels();
            foreach (var model in registrationFeeModels)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = model.Level.Name + " - " + model.Department.Name + " - " + model.Programme.Name;
                selectListItem.Value = model.Id.ToString();
                selectList.Add(selectListItem);
            }

            return Json(selectList);
        }

        public async Task<JsonResult> GetRegistrationFeeById(int id)
        {
            Expression<Func<RegistrationFee, bool>> selector = rf => rf.Id == id;
            RegistrationFee registrationFee = await _registrationFeeService.GetById(id, "Programme,Department,Level");

            return Json(new { accessCharge = registrationFee.AccessCharge, registrationFee = registrationFee.AmountPayable, amountPayable = registrationFee.AccessCharge + registrationFee.AmountPayable });
        }



    }
}
