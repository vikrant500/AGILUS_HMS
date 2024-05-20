using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    [Authorize(Roles = "Administrators,Customer")]
    public class HomeController : Controller
    {
        // GET: Home
        [Authorize(Roles = "Administrators,Customer")]
        public ActionResult Index()
        {
            if (User.IsInRole("Customer"))
                return RedirectToAction("List", "Reports");
            else 
                return RedirectToAction("Index", "Dashboard");

        }


        [Authorize(Roles = "Users")]
        public ActionResult OtherAction()
        {
            return View("Index", GetData("OtherAction"));
        }

        private Dictionary<string, object> GetData(string actionName)
        {
            Dictionary<string, object> dict
            = new Dictionary<string, object>();

            dict.Add("Action", actionName);
            dict.Add("User", HttpContext.User.Identity.Name);
            dict.Add("Authenticated", HttpContext.User.Identity.IsAuthenticated);
            dict.Add("Auth Type", HttpContext.User.Identity.AuthenticationType);
            dict.Add("In Users Role", HttpContext.User.IsInRole("Users"));
            return dict;
        }
    }
}