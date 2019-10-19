using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using LmsGateway.Domain.Users;

namespace LmsGateway.Web.Infrastructure
{
    public class PasswordValidator : PasswordValidator<User>
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
        {
            IdentityResult result = await base.ValidateAsync(manager, user, password);
            return result;
        }


    }
}
