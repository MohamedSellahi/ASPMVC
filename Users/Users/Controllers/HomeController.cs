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
using Users.Infrastructure;
using Users.Models;

namespace Users.Controllers
{
   public class HomeController : Controller
   {
      public AppUserManager UserManager
      {
         get { return HttpContext.GetOwinContext().Get<AppUserManager>(); }
      }
      public AppUser CurrentUser
      {
         get
         {
            return UserManager.FindByName(HttpContext.User.Identity.Name);
         }
      }

      // GET: Home
      [Authorize]
      public ActionResult Index()
      {
         return View(GetData(nameof(Index)));
      }

      private Dictionary<string, object> GetData(string actionName)
      {
         Dictionary<string, object> dict = new Dictionary<string, object>();
         dict.Add("Action", actionName);
         dict.Add("User", HttpContext.User.Identity.Name);
         dict.Add("Authenticated", HttpContext.User.Identity.IsAuthenticated);
         dict.Add("Auth type", HttpContext.User.Identity.AuthenticationType);
         dict.Add("In Users Role", HttpContext.User.IsInRole("Users"));
         return dict;
      }

      [Authorize]
      public ActionResult UserProps()
      {
         return View(CurrentUser);
      }

      [Authorize]
      [HttpPost]
      public async Task<ActionResult> UserProps(Cities city)
      {
         AppUser user = CurrentUser;
         user.City = city;
         user.SetCountryFromCity(city);
         await UserManager.UpdateAsync(user);
         return View(user); 
      }
   }
}