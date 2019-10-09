using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Moq;
using Xunit;
using Microsoft.AspNetCore.Identity;
using LmsGateway.Domain;
using LmsGateway.Web.Models;
using LmsGateway.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace LmsGateway.Web.Tests
{
    public class AccountControllerTest
    {
        private Mock<UserManager<User>> _userManager;
        private Mock<SignInManager<User>> _signInManager;

        public AccountControllerTest()
        {
            //_userManager = new Mock<UserManager<User>>();
            _userManager = MockUserManager.GetUserManager<User>();
            _signInManager = MockSignInManager.GetSignInManager<User>();

            //
            // Summary:
            //     Creates a new instance of Microsoft.AspNetCore.Identity.SignInManager`1.
            //
            // Parameters:
            //   userManager:
            //     An instance of Microsoft.AspNetCore.Identity.SignInManager`1.UserManager used
            //     to retrieve users from and persist users.
            //
            //   contextAccessor:
            //     The accessor used to access the Microsoft.AspNetCore.Http.HttpContext.
            //
            //   claimsFactory:
            //     The factory to use to create claims principals for a user.
            //
            //   optionsAccessor:
            //     The accessor used to access the Microsoft.AspNetCore.Builder.IdentityOptions.
            //
            //   logger:
            //     The logger used to log messages, warnings and errors.
            //_signInManager = new Mock<SignInManager<User>>(_userManager, null, null, null, null);

        }

        [Fact]
        public async Task CanSignupWhenUserModelIsValid()
        {
            //UserModel userModel = new UserModel() { Name = "Name", Email = "model@Email.com", Username = "model.Email", Password= "password", IAgree =true };
            //User user = new User() { Name = userModel.Name, Email = userModel.Email, UserName = userModel.Username };
            //_userManager.Setup(x => x.CreateAsync(user, userModel.Password)).ReturnsAsync(IdentityResult.Success);
            
            //AccountController accountController = new AccountController(_userManager.Object, _signInManager.Object);
            //var result = await accountController.Signup(userModel);

            //Assert.NotNull(result);
            //Assert.IsType<RedirectToActionResult>(result);
            

            //// Act
            //var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

        }

    }

    public static class MockUserManager
    {
        public static Mock<UserManager<TUser>> GetUserManager<TUser>()
            where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var passwordHasher = new Mock<IPasswordHasher<TUser>>();
            IList<IUserValidator<TUser>> userValidators = new List<IUserValidator<TUser>>
        {
            new UserValidator<TUser>()
        };
            IList<IPasswordValidator<TUser>> passwordValidators = new List<IPasswordValidator<TUser>>
        {
            new PasswordValidator<TUser>()
        };
            userValidators.Add(new UserValidator<TUser>());
            passwordValidators.Add(new PasswordValidator<TUser>());
            var userManager = new Mock<UserManager<TUser>>(store.Object, null, passwordHasher.Object, userValidators, passwordValidators, null, null, null, null);
            return userManager;
        }
    }

    public static class MockSignInManager
    {
        public static Mock<SignInManager<User>> GetSignInManager<TUser>() where TUser:class
        {
            Mock<UserManager<TUser>> userManager = MockUserManager.GetUserManager<TUser>();
            Mock<IHttpContextAccessor> contextAccessor = new Mock<IHttpContextAccessor>();
            Mock<IUserClaimsPrincipalFactory<TUser>> claimsFactory = new Mock<IUserClaimsPrincipalFactory<TUser>>();
            Mock<IOptions<IdentityOptions>> optionsAccessor = new Mock<IOptions<IdentityOptions>>();
            Mock<ILogger<SignInManager<TUser>>> logger = new Mock<ILogger<SignInManager<TUser>>>();

            return new Mock<SignInManager<User>>(userManager.Object, contextAccessor.Object,  claimsFactory.Object,  optionsAccessor.Object,  logger.Object);

        }

    }




}
