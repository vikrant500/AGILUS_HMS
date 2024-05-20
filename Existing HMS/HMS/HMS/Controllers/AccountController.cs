using HMS.Infrastructure;
using HMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        
        public async Task<ActionResult> Login(LoginModel details)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await UserManager.FindAsync(details.Name, details.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid name or password.");
                }
                else
                {
                    ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();

                    AuthManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, ident);
                    

                    return RedirectToAction("Index", "Home");
                }
            }
            return View(details);
        }
        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        [Authorize(Roles = "Administrators,Customer")]
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}