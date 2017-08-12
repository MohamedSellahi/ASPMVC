using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Infrastructure.Concrete;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers {
   public class AccountController : Controller {

      IAuthProvider _authProvider;

      public AccountController(IAuthProvider auth) {
         _authProvider = auth;
      }
      
      public ViewResult Login() {
         return View();
      }

      [HttpPost]
      public ActionResult Login(LoginViewModel model, string returnUrl) {

         if (ModelState.IsValid) {
            // try authentify
            // if ok redirect to admin/index
            if (_authProvider.Athenticate(model.UserName, model.Password)) {
               return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
            }
            else {
               // this message will be added to validation errors dictionnary
               ModelState.AddModelError("", "Incorrect username or password");
               return View();
            }
         }
         else {
            return View();
         }
      }

      // GET: Account
      public ActionResult Index() {
         return View();
      }
   }
}