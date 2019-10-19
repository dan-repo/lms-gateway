using LmsGateway.Core.Infrastructure;
using LmsGateway.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            Guard.NotNull(userManager, nameof(userManager));

            _userManager = userManager;
        }

        #region Utility

        private List<User> GetUsers()
        {
            return _userManager.Users.ToList();
        }

        private List<User> GetUnverifiedUsers()
        {
            return _userManager.Users.Where(x => x.EmailConfirmed == true && x.Verified == false).ToList();
        }
        
        #endregion

        public async Task<IActionResult> Index()
        {
            IList<User> users = GetUsers();
            return await Task.FromResult(View(users));
        }

        public async Task<IActionResult> Verify()
        {
            IList<User> users = GetUnverifiedUsers();
            return await Task.FromResult(View(users));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Verify(List<User> users)
        {
            if (users != null && users.Count > 0)
            {
                IList<User> verifiedUsers = users.Where(x => x.Verified == true).ToList();
                if (verifiedUsers != null && verifiedUsers.Count > 0)
                {
                    foreach (User verifiedUser in verifiedUsers)
                    {
                        User user = await _userManager.FindByIdAsync(verifiedUser.Id);
                        user.Verified = verifiedUser.Verified;
                        await _userManager.UpdateAsync(user);
                    }
                }
            }

            IList<User> unverifiedUser = GetUnverifiedUsers();
            return await Task.FromResult(PartialView("_verify/_unverifiedUsers", unverifiedUser));
        }





    }
}
