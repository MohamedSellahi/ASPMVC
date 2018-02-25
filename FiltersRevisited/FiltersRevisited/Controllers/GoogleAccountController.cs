using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FiltersRevisited.Controllers {
   public class GoogleAccountController : Controller {
      // GET: GoogleAccount
      public ActionResult Login() {
         return View();
      }

      [HttpPost]
      public ActionResult Login(string username, string password, string returnUrl) {
         // simulate google login 
         if (username.EndsWith("@google.com") && password == "secret") {
            FormsAuthentication.SetAuthCookie(username, false);
            return Redirect(returnUrl ?? Url.Action("index", "Home"));
         }
         else {
            ModelState.AddModelError("", "incorrect login or password");
            return View();
         }
      }

   }
}