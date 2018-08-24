using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Users.Infrastructure;
using Users.Models;

namespace Users.Controllers
{
   [Authorize(Roles = "Administrators")]
   public class RoleAdminController : Controller
   {
      private AppRoleManager RoleManager => HttpContext
         .GetOwinContext().Get<AppRoleManager>();

      private AppUserManager UserManager => HttpContext
        .GetOwinContext().Get<AppUserManager>();

      // GET: RoleAdmin
      public ActionResult Index()
      {
         return View(RoleManager.Roles);
      }

      public ActionResult Create()
      {
         return View();
      }

      [HttpPost]
      public async Task<ActionResult> Create([Required] string name)
      {
         if (ModelState.IsValid)
         {
            IdentityResult result = await RoleManager.CreateAsync(new AppRole(name));
            if (result.Succeeded)
            {
               return RedirectToAction(nameof(Index));
            }
            else
            {
               AddErrorsFromResult(result);
            }
         }
         return View(name);
      }

      [HttpPost]
      public async Task<ActionResult> Delete(string id)
      {
         AppRole role = await RoleManager.FindByIdAsync(id);
         if (role != null)
         {
            var result = await RoleManager.DeleteAsync(role);
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
            return View("Error", new string[] { "Role not found" });
         }
      }

      public async Task<ActionResult> Edit(string id)
      {
         AppRole role = await RoleManager.FindByIdAsync(id);
         if (role != null)
         {
            string[] memberIds = role.Users.Select(u => u.UserId).ToArray();
            var members = UserManager.Users.Where(u => memberIds.Any(i => i == u.Id)).AsEnumerable();
            var nonMembers = UserManager.Users.Except(members);
            return View(new RoleEditModel
            {
               Role = role,
               Members = members, 
               NonMembers = nonMembers
            });
         }
         return View(new RoleEditModel());
      }

      [HttpPost]
      public async Task<ActionResult> Edit(RoleModificationModel model)
      {
         IdentityResult result;
         if (ModelState.IsValid)
         {
            foreach (string userId in model.IdsToAdd ?? new string[] { })
            {
               result = await UserManager.AddToRoleAsync(userId, model.RoleName);
               if (!result.Succeeded)
               {
                  return View("Error", result.Errors);
               }
            }
            foreach (string userId in model.IdsToDelete ?? new string[] { })
            {
               result = await UserManager.RemoveFromRoleAsync(userId,
               model.RoleName);
               if (!result.Succeeded)
               {
                  return View("Error", result.Errors);
               }
            }
            return RedirectToAction(nameof(Index));
         }
         return View("Error", new string[] { "Role not found" });
      }



      /// <summary>
      /// Adds the Identity errors to the ModelState
      /// </summary>
      /// <param name="result"></param>
      private void AddErrorsFromResult(IdentityResult result)
      {
         foreach (var err in result.Errors)
         {
            ModelState.AddModelError("", err);
         }
      }
   }
}