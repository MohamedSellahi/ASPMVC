using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Filters.Controllers {
   public class GoogleAccountController : Controller {

      public ActionResult Login() {
         return View();
      }

      [HttpPost]
      public ActionResult Login(string username, string password, string returnUrl) {
         if (username.EndsWith("@google.com") && password == "12345") {
            FormsAuthentication.SetAuthCookie(username, false);
            return Redirect(returnUrl ?? Url.Action("Index", "Home"));
         }
         else {
            ModelState.AddModelError("", "Incorrect username or password");
            return View();
         }
      }


      // GET: GoogleAccount
      public ActionResult Index() {
         return View();
      }
   }
}