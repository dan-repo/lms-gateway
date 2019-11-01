using LmsGateway.Core.Data;
using LmsGateway.Data;
using LmsGateway.Domain.Registrations;
using LmsGateway.Services.Configuration;
using LmsGateway.Services.Registrations;
using LmsGateway.Tests;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LmsGateway.Services.Tests
{
    public class RegistrationServiceTest
    {
        private IRegistrationService _registrationService;
        private IRepository<Registration> _registrationRepository;
        
        public RegistrationServiceTest()
        {
            _registrationRepository = new EFRepository<Registration>(Db.GetDataContext());
            _registrationService = new RegistrationService(_registrationRepository);
        }

        [Fact]
        public async Task Register_Should_SaveRegistrationEntitySuccessfully_WhenRegistrationObjectIsSet()
        {
            string userId = "1";
            Level level = new Level() {  Name = "Year 1" };
            Department department = new Department() { Name = "Business Administration" };
            Programme programme = new Programme() { Name = "Undergraduate Studies" };
            Semester semester = new Semester() { Name = "1st Semester", };
            Session session = new Session() { StartDate=DateTime.UtcNow, EndDate= DateTime.UtcNow.AddMonths(4), CreatedBy = userId, CreatedOn= DateTime.UtcNow };

            RegistrationPeriod registrationPeriod = new RegistrationPeriod()
            {
                CreatedBy = "",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMonths(4),
                Semester = semester,
                Session = session,
                CreatedOn = DateTime.UtcNow,
            };

            RegistrationFee registrationFee = new RegistrationFee()
            {
                AccessCharge = 5000,
                AmountPayable = 120000,
                CanMakePartPayment = false,
                CreatedBy = "1",
                CreatedOn = DateTime.UtcNow,
                Department = department,
                Level = level,
                Programme = programme,
                RegistrationPeriod = registrationPeriod,
                
            };

            Registration registration = new Registration()
            {
                StudentId = userId,
                RegisteredOn = DateTime.UtcNow,
                RegistrationFee = registrationFee,
                RegistrationPeriod = registrationPeriod,
                Details = new List<RegistrationDetail>()
                {
                    new RegistrationDetail() { AmountPaid = 120000, PaymentMethod = "paystack", PaymentStatus = 1, RegisteredOn = DateTime.UtcNow }
                }
            };

            Registration newReg = await _registrationService.RegisterAsync(registration);

            Assert.NotNull(newReg);
            Assert.NotNull(newReg.Details);
            Assert.True(newReg.Details.Count > 0);
            Assert.True(newReg.Id > 0);
            Assert.Equal(registration.RegisteredOn, newReg.RegisteredOn);
        }
    }




}
