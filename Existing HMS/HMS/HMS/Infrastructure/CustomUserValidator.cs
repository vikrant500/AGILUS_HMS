using HMS.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HMS.Infrastructure
{
    public class CustomUserValidator : UserValidator<AppUser>
    {
        public CustomUserValidator(AppUserManager mgr)
        : base(mgr)
        {
        }

        public override async Task<IdentityResult> ValidateAsync(AppUser user)
        {
            IdentityResult result = await base.ValidateAsync(user);
            return result;
        }
    }
}