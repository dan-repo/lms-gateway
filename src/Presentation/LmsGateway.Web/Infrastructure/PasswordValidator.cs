using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Domain;
using Microsoft.AspNetCore.Identity;

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
