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
   }
}