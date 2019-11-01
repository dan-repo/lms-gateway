using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Moq;
using LmsGateway.Services.Registrations;
using LmsGateway.Domain.Registrations;
using LmsGateway.Web.Areas.Student.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using LmsGateway.Web.Areas.Student.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LmsGateway.Web.Tests.Student
{
    public class RegistrationControllerTest
    {
        private Mock<IRegistrationFeeService> _feeService;
        private Mock<IRegistrationPeriodService> _periodService;

        public RegistrationControllerTest()
        {
            _feeService = new Mock<IRegistrationFeeService>();
            _periodService = new Mock<IRegistrationPeriodService>();
        }

        [Fact]
        public async Task Index_CanPopulateRegistrationIndexPage()
        {
            //List<RegistrationPeriod> periods = new List<RegistrationPeriod>()
            //{
            //     new RegistrationPeriod()
            //        {
            //            Semester = new Semester() { Name = "1st Semester"},
            //            Session = new Session() { Name = "2019/2020 Academic Session"}
            //        }
            //};

            //_periodService.Setup(x => x.GetAll("Session, Semester")).Returns(Task.FromResult(periods));

            //RegistrationController registrationController = new RegistrationController(_feeService.Object, _periodService.Object);

            //IActionResult result = await registrationController.Index();
            //ViewResult viewResult = (ViewResult)result;

            //Assert.NotNull(result);
            //Assert.NotNull(viewResult);
            //Assert.IsType<RegistrationModel>(viewResult.ViewData.Model);

            //RegistrationModel registrationModel = (RegistrationModel)viewResult.ViewData.Model;

            //Assert.NotNull(registrationModel);
            //Assert.NotNull(registrationModel.Periods);
            //Assert.NotNull(registrationModel.RegistrationPeriods);
            //Assert.Equal(periods[0].Session.Name, registrationModel.RegistrationPeriods[0].Session.Name);
        }

        [Fact]
        public async Task GetRegistrationFeeByPeriodId_CanGetRegistrationFee()
        {
            List<RegistrationFee> registrationFees = new List<RegistrationFee>()
            {
                 new RegistrationFee()
                    {
                        AmountPayable = 120000,
                        AccessCharge = 5000,
                        CanMakePartPayment = true,
                        CreatedOn = DateTime.UtcNow,
                        CreatedBy = "1",

                        RegistrationPeriodId = 1,
                        LevelId = 1,
                        DepartmentId = 1,
                        ProgrammeId = 1,

                        Level = new Level() { Id = 1, Name = "1st Year" },
                        Programme = new Programme() {Id = 1, Name = "Undergraduate Studies" },
                        Department = new Department() { Id = 1, Name = "Business Administration" },
                     //RegistrationPeriod { get; set; }
                 }
            };

            _feeService.Setup(x => x.GetByPeriodId(1, "Programme,Department,Level")).Returns(Task.FromResult(registrationFees));

            //RegistrationController registrationController = new RegistrationController(_feeService.Object, _periodService.Object);
            //JsonResult result = await registrationController.GetRegistrationFeeByPeriodId(1);

            //Assert.NotNull(result);
            //Assert.NotNull(result.Value);
            //Assert.IsType<SelectList>(result.Value);

            //SelectList selectList = (SelectList)result.Value;
        }




    }
}
