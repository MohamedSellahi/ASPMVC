using System.Web.Mvc;
using Users.Infrastructure;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Users.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace Users.Controllers
{
   [Authorize(Roles = "Administrators")]
   public class AdminController : Controller
   {
      public AppUserManager UserManager
      {
         get
         {
            return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
         }
      }

      public ActionResult Index()
      {
         return View(UserManager.Users);
      }

      public ActionResult Create()
      {
         return View();
      }

      [HttpPost]
      public async Task<ActionResult> Create(CreateModel model)
      {
         if (ModelState.IsValid)
         {
            AppUser user = new AppUser { UserName = model.Name, Email = model.Email };
            IdentityResult result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
               return RedirectToAction(nameof(Index));
            }
            else
            {
               AddErrorsFromResult(result);
            }
         }
         return View(model);
      }

      public async Task<ActionResult> Edit(string Id)
      {
         AppUser user = await UserManager.FindByIdAsync(Id);
         if (user != null)
         {
            return View(user);
         }
         else
         {
            return RedirectToAction(nameof(Index));
         }
      }

      [HttpPost]
      public async Task<ActionResult> Edit(string id, string email, string password)
      {
         AppUser user = await UserManager.FindByIdAsync(id);
         if (user != null)
         {
            user.Email = email;
            IdentityResult validEmail
                = await UserManager.UserValidator.ValidateAsync(user);
            if (!validEmail.Succeeded)
            {
               AddErrorsFromResult(validEmail);
            }
            IdentityResult validPass = null;
            if (password != string.Empty)
            {
               validPass
                   = await UserManager.PasswordValidator.ValidateAsync(password);
               if (validPass.Succeeded)
               {
                  user.PasswordHash =
                      UserManager.PasswordHasher.HashPassword(password);
               }
               else
               {
                  AddErrorsFromResult(validPass);
               }
            }
            if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded
                    && password != string.Empty && validPass.Succeeded))
            {
               IdentityResult result = await UserManager.UpdateAsync(user);
               if (result.Succeeded)
               {
                  return RedirectToAction("Index");
               }
               else
               {
                  AddErrorsFromResult(result);
               }
            }
         }
         else
         {
            ModelState.AddModelError("", "User Not Found");
         }
         return View(user);
      }

      public async Task<ActionResult> Delete(string id)
      {
         AppUser user = await UserManager.FindByIdAsync(id);
         if (user != null)
         {
            var result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
               return RedirectToAction(nameof(Index));
            }
            else
            {
               return View("Error", result.Errors);
            }
         }
         else
         {
            return View("Error", new string[] { "User not Found" });
         }
      }

      #region Helpers
      /// <summary>
      /// reports the errors from <see cref="UserManager"/> calls to the model state 
      /// </summary>
      /// <param name="result"></param>
      private void AddErrorsFromResult(IdentityResult result)
      {
         foreach (string error in result.Errors)
         {
            ModelState.AddModelError("", error);
         }
      }

      #endregion
   }
}