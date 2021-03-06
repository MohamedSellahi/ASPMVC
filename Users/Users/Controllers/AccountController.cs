﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Users.Infrastructure;
using Users.Models;

namespace Users.Controllers
{
   [Authorize]
   public class AccountController : Controller
   {
      private AppUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<AppUserManager>();

      private IAuthenticationManager AuthManager => HttpContext.GetOwinContext().Authentication;

      [AllowAnonymous]
      [HttpGet]
      public ActionResult Login(string returnUrl)
      {
         if (HttpContext.User.Identity.IsAuthenticated)
         {
            return View("Error", new string[] { "Access Denied" });
         }
         ViewBag.returnUrl = returnUrl;
         return View();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      [AllowAnonymous]
      public async Task<ActionResult> Login(LoginModel details, string returnUrl)
      {
         if (ModelState.IsValid)
         {
            AppUser user = await UserManager.FindAsync(details.Name, details.Password);
            if (user == null)
            {
               ModelState.AddModelError("", "Invalid name or password");
            }
            else
            {
               ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
               AuthManager.SignOut();
               AuthManager.SignIn(new AuthenticationProperties
               {
                  IsPersistent = true
               }, ident);
               return Redirect(returnUrl);
            }
         }
         ViewBag.returnUrl = returnUrl;
         return View(details);
      }

      [Authorize]
      public ActionResult Logout()
      {
         AuthManager.SignOut();
         return RedirectToAction("Index", "Home");
      }
   }
}