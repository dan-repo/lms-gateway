using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Moq;
using Xunit;
using Microsoft.AspNetCore.Identity;
using LmsGateway.Domain.Users;
using LmsGateway.Web.Areas.Student.Controllers;
using Microsoft.AspNetCore.Mvc;
using LmsGateway.Web.Framework;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.DependencyInjection;

namespace LmsGateway.Web.Tests.Student
{
    public class AccountControllerTest
    {
        private Mock<UserManager<User>> _userManager;
        private Mock<SignInManager<User>> _signInManager;

        public AccountControllerTest()
        {
            _userManager = MockUserManager.GetUserManager<User>();
            _signInManager = MockSignInManager.GetSignInManager<User>();
        }

        [Fact]
        public async Task c()
        {
            HostingEnvironment hostEnvironment = new HostingEnvironment();
            hostEnvironment.ContentRootPath = "C:\\Users\\LENOVO\\Documents\\Repositories\\lms-gateway\\src\\Presentation\\LmsGateway.Web";
            hostEnvironment.ContentRootFileProvider = new PhysicalFileProvider(hostEnvironment.ContentRootPath);
            hostEnvironment.EnvironmentName = "Development";

            Startup startup = new Web.Startup(hostEnvironment);
            IServiceCollection services = new ServiceCollection();
            startup.ConfigureServices(services);

            AccountController accountController = new AccountController(_userManager.Object, _signInManager.Object);

            IActionResult response = await accountController.Subscription();
            //var result = (IActionResult)response;

            //Assert.NotNull(result);
            Assert.NotNull(response);

            //Assert.Equal(7, Core.UI.Widget.Value);
            
            //Assert.IsType<ViewViewComponentResult>(response);
            //Assert.IsType<PaymentInfoModel>(result.ViewData.Model);
            //Assert.True((result.ViewData.Model as PaymentInfoModel).IconUrl != null);
        }




    }
}
