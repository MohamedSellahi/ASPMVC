using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Users.Infrastructure;
using Users.Models;

namespace Users
{
   /// <summary>
   /// This is the startUp Class 
   /// </summary>
   public class IdentityConfig
   {
      public void Configuration(IAppBuilder app)
      {
         app.CreatePerOwinContext<AppIdentityDbContext>(AppIdentityDbContext.Create);
         app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);

         app.UseCookieAuthentication(new CookieAuthenticationOptions
         {
            AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            LoginPath = new PathString("/Acount/Login")
         });
      }
   }
}